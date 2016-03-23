angular.module('app')
.service('ProviderBackEndService', function ($http) {
    return {
        save: function (stuff) {
            var url = 'api/provider/individual'
            return $http.post(url, stuff);
        }
    };
})
.controller('individualCreateFormCtrl', function ($scope, GlobalsService, ProviderBackEndService, $stateParams, $window) {

    $scope.model = createModel($stateParams.type); // resolve the model so this one can be used for editing stuff 
    
    $scope.lists = {
        regions: GlobalsService.get('regions'),
        freelanceStatuses: GlobalsService.get('freelanceStatuses'),
        employmentStatuses: GlobalsService.get('employmentStatuses'),
        titles: GlobalsService.get('titles'),
        offices: GlobalsService.get('offices'),
        legalForms: GlobalsService.get("legalForms")
    };
    
    $scope.save = function () {
        ProviderBackEndService.save($scope.model)
        .then(success, failure);

        function success() {
            toastr.info('Информация успешно сохранена');
            $window.history.back();
        }

        function failure() {
            toastr.error('Я ошипко');
        }
    };

    $scope.cancel = function () {
        $window.history.back();
    };

    function createModel(type) {

        // TODO: create a friendly start model 
        return {
            type: { id: type.id, name: type.title },
            personName: {
                name: { firstName : null, middleName : null, lastName : null }
            },
            details: {
                regionId: 2,            // Russia,
                timeDifference: 0,
                legalFormId: 1,         // individual,
                worksNightly: false 
            },
            freelance: {
                statusId : 1            // unchecked
            },
            employment: {},
            emails: [{}],
            telephones: [
                {
                     
                    typeId: 2           // mobile
                }
            ],
            otherContacts: [],
            services : []
        };
    }

});