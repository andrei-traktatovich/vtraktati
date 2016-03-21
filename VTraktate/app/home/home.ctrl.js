angular.module('app')
.controller('homeCtrl', ['$scope', 'User', function ($scope, User) {
    console.log('home controller');
    var user = User.get();
    $scope.userName = user.naming.addressName || user.naming.fullName;
    
}]);