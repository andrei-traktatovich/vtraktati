angular.module('app')
.factory('FreelanceService', function ($modal, $q, GlobalsService) {
    return {
        changeFreelance: changeFreelance
    };
    function changeFreelance(item) {
        var freelance = (item && item.freelance) || {},
            deferred = $q.defer();

        var modalInstance = $modal.open({
            templateUrl: 'app/providers/freelance/changeFreelance.dlg.tpl.html',
            resolve: {
                data: function () {
                    return {
                        id: item.id,
                        name: item.name,
                        freelance: freelance,
                        lists: {
                            freelanceStatuses: GlobalsService.get('freelanceStatuses')
                        }
                    };
                }
            },
            controllerAs: 'model',
            controller: function (data, $http, $modalInstance) {
                var model = this;
                model.name = data.name;
                model.lists = data.lists;
                model.freelance = data.freelance || {};
                model.freelance.startDate = new Date();

                model.save = function () {
                    var url = '/api/provider/' + data.id + '/freelance';
                    $http.post(url, model.freelance)
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