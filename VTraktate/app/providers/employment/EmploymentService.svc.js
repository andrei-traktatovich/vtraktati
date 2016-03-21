angular.module('app')
.factory('EmploymentService', function ($modal, $q) {
    return {
        changeEmployment: changeEmployment
    };
    function changeEmployment(item) {
        var employment = (item && item.employment) || {},
            deferred = $q.defer();

        var modalInstance = $modal.open({
            templateUrl: 'app/providers/employment/changeEmployment.dlg.tpl.html',
            resolve: {
                providerId: function () { return item.id; },
                employment: function () { return employment; }
            },
            controllerAs: 'model',
            controller: function (providerId, employment, $http, $modalInstance) {
                var model = this;
                model.providerId = providerId;
                model.employment = employment || {};
                model.employment.startDate = new Date();

                model.save = function () {
                    var url = '/api/provider/' + providerId + '/employment';
                    $http.post(url, model.employment)
                    .success(success)
                    .error(failure);

                    function success() {
                        toastr.info('данные успешно сохранены')
                        $modalInstance.close(model.employment);
                    }

                    function failure() {
                        toastr.error('ошипко');
                    }
                }

                model.cancel = function () {
                    $modalInstance.dismiss();
                }
            }
        });

        modalInstance.result.then(function (result) {
            return deferred.resolve(result);
        });

        return deferred.promise;
    }
});