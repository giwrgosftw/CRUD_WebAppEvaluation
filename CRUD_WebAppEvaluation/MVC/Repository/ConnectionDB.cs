using CRUD_WebAppEvaluation.DbApplicationContext;

namespace CRUD_WebAppEvaluation.MVC.Repository
{
    public class ConnectionDB
    {
        protected readonly CrudwebAppEvaluationDbContext db;

        public ConnectionDB()
        {
            db = new CrudwebAppEvaluationDbContext();
        }
    }
}
