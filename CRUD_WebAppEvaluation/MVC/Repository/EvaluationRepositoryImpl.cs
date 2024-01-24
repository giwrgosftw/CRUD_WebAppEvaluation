using CRUD_WebAppEvaluation.DbApplicationContext;

namespace CRUD_WebAppEvaluation.MVC.Repository
{
    public class EvaluationRepositoryImpl : ConnectionDB, EvaluationRepository
    {
        private readonly ILogger<EvaluationRepositoryImpl> _logger;

        public EvaluationRepositoryImpl(ILogger<EvaluationRepositoryImpl> logger)
        {
            _logger = logger;
        }

        public Evaluation FindById(int id)
        {
            try
            {
                return db.Evaluations.Where(v => v.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[EvaluationRepository:FindById()] Exception: {Message}", ex.Message);
                throw;
            }
        }

        public List<Evaluation> FindAll()
        {
            try
            {
                return db.Evaluations.ToList(); // use .ToList() since small dataset
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[EvaluationRepository:Add()] Exception: {Message}", ex.Message);
                throw;
            }
        }

        public void Add(Evaluation evaluation)
        {
            try
            {
                db.Evaluations.Add(evaluation);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[EvaluationRepository:Add()] Exception: {Message}", ex.Message);
                throw;
            }
        }

        public void Update(Evaluation existingEvaluation, Evaluation updatedEvaluation)
        {
            try
            {
                // Update the properties of the existing entity with the new values
                db.Entry(existingEvaluation).CurrentValues.SetValues(updatedEvaluation);
                // Save the changes to the database
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[EvaluationRepository:Update()] Exception: {Message}", ex.Message);
                throw;
            }
        }

        public void Delete(Evaluation evaluation)
        {
            try
            {
                db.Evaluations.Remove(evaluation);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[EvaluationRepository:Delete()] Exception: {Message}", ex.Message);
                throw;
            }
        }

        /// <summary> Logs the inner exception and its innermost exception, if available. </summary>
        /// <param name="ex"> The exception message for which to log inner exceptions. </param>
        protected void GetInnerException(Exception ex)
        {
            if (ex.InnerException != null)
            {
                _logger.LogError($"InnerException: {ex.InnerException?.Message}");

                // Check for InnerException's InnerException
                if (ex.InnerException.InnerException != null)
                {
                    _logger.LogError($"InnerException.InnerException: {ex.InnerException.InnerException?.Message}");
                }
            }
        }
    }
}
