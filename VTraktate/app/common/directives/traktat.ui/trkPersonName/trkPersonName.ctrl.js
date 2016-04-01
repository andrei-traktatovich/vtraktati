
angular.module("traktat.ui")
.directive("trkPersonName", function() {
    return {
        templateUrl: "app/common/directives/traktat.ui/trkPersonName/trkPersonName.tpl.html",
        scope: {
            model: "=personName",
            personId: "=personId"
        },
        controller: "PersonNameController"
    };

})
.controller("PersonNameController", function($scope, PersonNamePreview) {

    $scope.service = PersonNamePreview;

        $scope.model = $scope.model || {
            firstName: "",
            middleName: "",
            lastName: "",

            fullName: "",
            address: "",
            initials: "",
            alternateName: ""
        };

        $scope.fixCase = fixCase;

        function fixCase() {
            $scope.model.firstName = ($scope.model.firstName || "").toTitleCase();
            $scope.model.middleName = ($scope.model.name.middleName || "").toTitleCase();
            $scope.model.lastName = ($scope.model.name.lastName || "").toTitleCase();
            $scope.model.alternateName = ($scope.model.alternateName || "").toTitleCase();
        };

        var updateName = function() {

        $scope.model.fullName = makeFullName($scope.model);
        $scope.model.address = makeAddress($scope.model);
        $scope.model.initials = makeInitials($scope.model);
    };

    function makeFullName(name) {
        return [name.lastName, name.firstName, name.middleName].join(" ");
    }

    function makeAddress(name) {
        return (name.firstName + " " + name.middleName);
    }

    function makeInitials(name) {
        var result = name.lastName + " " + initial(name.firstName);
        if (name.middleName !== "")
            result = result + " " + initial(name.middleName);
        return result;

        function initial(item) {
            if (item && item !== "" && item.length > 1)
                return item[0] + ".";
            else
                return "";
        }
    }

    $scope.$watch("model.lastName", updateName);
    $scope.$watch("model.middleName", updateName);
    $scope.$watch("model.firstName", updateName);

});