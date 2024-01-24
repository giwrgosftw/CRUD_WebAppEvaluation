// Module
var app;
(function () {
    app = angular.module("RESTClientModule", [])
})();

// Service Constructor
app.service("CRUD_EvaluationService", function ($http) {
    this.getEvaluationByIdPost = function (evaluation) {
        return $http.post("/api/evaluation/getEvaluationByIdPost", evaluation);
    };
    this.getEvaluationByIdGet = function (id) {
        return $http.get("/api/evaluation/getEvaluationByIdGet/" + id);
    };

    this.getAllEvaluations = function () {
        return $http.get("/api/evaluation/getAllEvaluations");
    };

    this.addNewEvaluation = function (evaluation) {
        return $http.post("/api/evaluation/addNewEvaluation", evaluation);
    };

    this.updateEvaluation = function (evaluation) {
        return $http.post("/api/evaluation/updateEvaluation", evaluation);
    };

    this.deleteEvaluation = function (id) {
        return $http.post("/api/evaluation/deleteEvaluation?id=" + id);
    };
});

export default app;
