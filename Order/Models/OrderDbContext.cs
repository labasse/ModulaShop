using System.Data.SqlClient;
using Dapper;

namespace Order.Models
{
    public class OrderDbContext : IOrderDbContext
    {
        private string connectionString;
        public OrderDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<OrderEntity> Orders
        {
            get
            {
                using var cnct = new SqlConnection(connectionString);
                
                cnct.Open();
                return cnct.Query<OrderEntity>(@"
                    SELECT 
                        Id,
                        State AS Status,
                        ShippingFees,
                        Validated,
                        Paid AS TotalPaid
                    FROM Orders 
                ");
            }
        }

        public async Task<OrderEntity?> GetOrderByIdAsync(int id)
        {
            using var cnct = new SqlConnection(connectionString);
            var idOrderParam = new { IdOrder = id };

            await cnct.OpenAsync();
            var order = await cnct.QueryFirstOrDefaultAsync<OrderEntity>(@"
                    SELECT 
                        Id,
                        State AS Status,
                        ShippingFees,
                        Validated,
                        Paid AS TotalPaid
                    FROM Orders 
                    WHERE Id=@IdOrder
                ", idOrderParam);
            if(order == null)
            {
                return null;
            }
            order.Lines = await cnct.QueryAsync<OrderLine>(
                "SELECT * FROM OrderLines WHERE OrderId=@IdOrder",
                idOrderParam
            );
            List<OrderCmd> actions = new();
            
            using var reader = await cnct.ExecuteReaderAsync(
                "SELECT [Type], * FROM OrderActions WHERE OrderId=@IdOrder",
                idOrderParam
            );
            var mapSetInfos = reader.GetRowParser<OrderCmdSetInfos>();
            var mapPay      = reader.GetRowParser<OrderCmdPay>();
            var mapShip     = reader.GetRowParser<OrderCmdShip>();
            while(reader.Read())
            {
                switch(reader.GetInt32(0))
                {
                    case 0: actions.Add(mapSetInfos(reader)); break;
                    case 1: actions.Add(mapPay     (reader)); break;
                    case 2: actions.Add(mapShip    (reader)); break;
                }
            }
            order.Actions = actions;
            return order;
        }
    }
}
