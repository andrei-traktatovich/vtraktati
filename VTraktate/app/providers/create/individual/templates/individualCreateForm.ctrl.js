angular.module('app')
.service('ProviderBackEndService', function ($http) {
    return {
        save: function (stuff) {
            var url = 'api/provider/individual'
            return $http.post(url, stuff);
        }
    };
})
.controller('individualCreateFormCtrl', function ($rootScope, $scope, GlobalsService, ProviderBackEndService, $stateParams, notifyClient, handleHttpError) {

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
        .then(success, handleHttpError("Невозможно сохранить исполнителя"));

        function success() {
            notifyClient.success("Информация успешно сохранена", `Информация об исполнителе ${$scope.model.personName.fullName} успешно сохранена`);
            back();
        }
    };

        $scope.cancel = back;

        function back() {
            $rootScope.back();
        }

    function createModel(type) {

        // TODO: create a friendly start model 
        return {
            type: { id: type.id, name: type.title },
            personName: {
                name: { firstName : "", middleName : "", lastName : "" }
            },
            details: {
                regionId: 2,            // Russia,
                timeDifference: 0,
                legalFormId: 1,         // individual 
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