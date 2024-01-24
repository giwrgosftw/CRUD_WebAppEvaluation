import app from './EvaluationService.js'

// Controller Constructor
app.controller("CRUD_ControllerEvaluations", function ($scope, $window, $log, CRUD_EvaluationService) {

    $scope.getEvaluationByIdPost = function (evaluation) {
        CRUD_EvaluationService.getEvaluationByIdPost(evaluation)
            .then(function (response) {
                $scope.evaluation = response.data;
            })
            .catch(function (error) {
                $log.error(error);
            });
    };

    $scope.getEvaluationByIdGet = function (id) {
        CRUD_EvaluationService.getEvaluationByIdGet(id)
            .then(function (response) {
                $scope.evaluation = response.data; // NOTE: evaluation => same variable name in the html
            })
            .catch(function (error) {
                $log.error(error);
            });
    };

    $scope.getAllEvaluations = function () {
        CRUD_EvaluationService.getAllEvaluations()
            .then(function (response) {
                $scope.evaluations = response.data;
            })
            .catch(function (error) {
                $log.error(error);
            });
    };

    $scope.addNewEvaluation = function (evaluation) {
        CRUD_EvaluationService.addNewEvaluation(evaluation)
            .then(function (response) {
                $scope.getAllEvaluations(); // Refresh the list after adding
                $log.info(response.data.message);
                $('#message').text(response.data.message);
            })
            .catch(function (error) {
                if (error.status === 400) {
                    // Handle Bad Request (400) - Validation or client-side error
                    $log.error('Bad Request: ' + error.data.message);
                    $('#message').text('Bad Request: ' + error.data.message);
                } else if (error.status === 404) {
                    // Handle Not Found (404) - Resource not found
                    $log.error('Resource not found: ' + error.data.message);
                    $('#message').text('Resource not found: ' + error.data.message);
                } else {
                    // Handle other server errors
                    $log.error('Server error: ' + error.data.message);
                    $('#message').text('Server error: ' + error.data.message);
                }
            });
    };

    $scope.updateEvaluation = function (evaluation) {
        CRUD_EvaluationService.updateEvaluation(evaluation)
            .then(function (response) {
                $scope.getAllEvaluations(); // Refresh the list after updating
                $log.info(response.data.message);
                $('#message').text(response.data.message);
            })
            .catch(function (error) {
                if (error.status === 400) {
                    // Handle Bad Request (400) - Validation or client-side error
                    $log.error('Bad Request: ' + error.data.message);
                    $('#message').text('Bad Request: ' + error.data.message);
                } else if (error.status === 404) {
                    // Handle Not Found (404) - Resource not found
                    $log.error('Resource not found: ' + error.data.message);
                    $('#message').text('Resource not found: ' + error.data.message);
                } else {
                    // Handle other server errors
                    $log.error('Server error: ' + error.data.message);
                    $('#message').text('Server error: ' + error.data.message);
                }
            });
    };

    $scope.deleteEvaluation = function (id) {
        CRUD_EvaluationService.deleteEvaluation(id)
            .then(function (response) {
                $scope.getAllEvaluations(); // Refresh the list after deleting
                $log.info(response.data.message);
                $('#message').text(response.data.message);
            })
            .catch(function (error) {
                if (error.status === 404) {
                    // Handle Not Found (404) - Resource not found
                    $log.error('Resource not found: ' + error.data.message);
                    $('#message').text('Resource not found: ' + error.data.message);
                } else {
                    // Handle other server errors
                    $log.error('Server error: ' + error.data.message);
                    $('#message').text('Server error: ' + error.data.message);
                }
            });
    };

    //////////////////Extra Controllers/////////////////////////////
    $scope.selectEvaluation = function (selectedEvaluation) { // $scope.selectedEvaluation => where the select button is
        $scope.theEvaluation = angular.copy(selectedEvaluation); // $scope.theEvaluation => the field name of the form which we want to paste
    };

    $scope.deleteAndReset = function (evaluation) {
        $scope.deleteEvaluation(evaluation.id); // Delete the evaluation
        $scope.resetForm();
    };

    // Function to reset the form or model
    $scope.resetForm = function () {
        // You can reset the properties of the model or any other logic to clear the form
        $scope.evaluation = null;
    };

    $scope.deleteAndBackHome = function (evaluation) {
        $scope.deleteEvaluation(evaluation.id); // Delete the evaluation
        // After delete operation, redirect to home page
        window.location.href = "/";
    };

    /////////////////
    var getQueryString = function (field, url) {
        var href = url ? url : window.location.href;
        var reg = new RegExp('/([0-9]+)$', 'i'); // catch the number after the last slash '/'
        var string = reg.exec(href);
        return string ? string[1] : null;
    };

    $scope.getEvaluationIdByURL = function () {
        $scope.theEvalutionId = getQueryString('id')
    }

    $scope.getEvaluationDetails = function (id) {
        CRUD_EvaluationService.getEvaluationByIdGet(id)
            .then(function () {
                $window.open('/Home/EvaluationDetails/' + id, '_blank',); // _blank = open the URL in a new window or tab
            })
            .catch(function (error) {
                $log.error(error);
            });
    };

});
