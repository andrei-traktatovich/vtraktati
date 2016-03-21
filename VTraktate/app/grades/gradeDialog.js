(function () {

    angular.module('grades')
    .service('gradeDialog', gradeDialog);

    // TODO: tableParams to be resolved by the router to persist state? 
    function gradeDialog($modal, gradesService) {
        
        return {
            open: open
        };

        function open(model, config) {

            var modalInstance = $modal.open({
                templateUrl: 'app/grades/addUpdateGrade.dlg.tpl.html',
                controller: 'addUpdateGradeController',
                resolve: {
                    model: function () { return model; },
                    config: function () { return config }
                }
            });
                
            return modalInstance.result;
        }

    }
})();