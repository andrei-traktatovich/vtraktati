angular.module('app')
.factory('ProviderTypeService', function ($q, $http, $modal, GlobalsService) {
    
    var typeChanger = [
        makeInhouse,
        makeFreelance,
        null,
        makeContractor
    ];

    return {
        changeType: changeType
    };

    function changeType(item, typeId) {
        if (item.typeId === typeId)
            throw new Error('Статус ресурса и новый статус совпадают.');
        if (typeChanger[typeId])
            return typeChanger[typeId](item);
        else 
            throw new Error('Недействительный новый статус');
    }
    

    function makeInhouse(item) {
        var employment = {}, // default status ... 
            deferred = $q.defer();
        // TODO: REFACTOR 
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
                    var url = '/api/provider/' + providerId + '/hire';

                    $http.post(url, model.employment)
                    .success(success)
                    .error(failure);

                    function success() {
                        toastr.info('данные успешно сохранены');
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
        // call inhouse employment dialog
        // if resolved, 
        //  post to /provider/id/hire
    }

    function makeFreelance(item) {
        var freelance = (item && item.freelance) || {},
            lastEmployment = item.employment,
            deferred = $q.defer();

        lastEmployment.startDate = new Date();

        var modalInstance = $modal.open({
            templateUrl: 'app/providers/type/employeeFireDialog.dlg.tpl.html',
            resolve: {
                data: function () {
                    return {
                        id: item.id,
                        name: item.name,
                        freelance: freelance,
                        lastEmployment: lastEmployment,
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
                model.lastEmployment = data.lastEmployment;

                model.save = function () {
                    var url = '/api/provider/' + data.id + '/fire';
                    $http.post(url, { freelance: model.freelance, lastEmployment: lastEmployment })
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

    function makeContractor(item) {
        throw new Error('not implemented');
    }
})