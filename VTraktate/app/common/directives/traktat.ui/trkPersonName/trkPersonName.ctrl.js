
angular.module('traktat.ui')
.directive('trkPersonName', function() {
    return {
        templateUrl: 'app/common/directives/traktat.ui/trkPersonName/trkPersonName.tpl.html',
        scope: {
            model: '=personName',
            personId: '=personId'
        },
        controller: 'PersonNameController'
    };

})
.controller('PersonNameController', function($scope, PersonNamePreview) {

    $scope.service = PersonNamePreview;

    $scope.model = $scope.model || {
        name: {
            firstName: '',
            middleName: '',
            lastName: ''
        },
        fullName: '',
        address: '',
        initials: '',
        alternateName: ''
    };

    $scope.fixCase = function () {
        
        $scope.model.name = {
            firstName: ($scope.model.name.firstName || '').toTitleCase(),
            middleName: ($scope.model.name.middleName || '').toTitleCase(),
            lastName: ($scope.model.name.lastName || '').toTitleCase()
        };
        //$scope.$apply(function () {
        //    $scope.model.name = {
        //        firstName: $scope.model.name.firstName.toTitleCase(),
        //        middleName: $scope.model.name.middleName.toTitleCase(),
        //        lastName: $scope.model.name.lastName.toTitleCase()
        //    };
        //});
    };

    var updateName = function() {

        $scope.model.fullName = makeFullName($scope.model.name);
        $scope.model.address = makeAddress($scope.model.name);
        $scope.model.initials = makeInitials($scope.model.name);
    };

    function makeFullName(name) {
        return [name.lastName, name.firstName, name.middleName].join(' ');
    }

    function makeAddress(name) {
        return (name.firstName + ' ' + name.middleName);
    }

    function makeInitials(name) {
        var result = name.lastName + ' ' + initial(name.firstName);
        if (name.middleName !== '')
            result = result + ' ' + initial(name.middleName);
        return result;

        function initial(item) {
            if (item && item !== '' && item.length > 1)
                return item[0] + '.';
            else
                return '';
        }
    }

    $scope.$watch('model.name.lastName', updateName);
    $scope.$watch('model.name.middleName', updateName);
    $scope.$watch('model.name.firstName', updateName);

});