(function () {

    angular.module('thirdPartyServices', [])
        .value('_', _)
        .value('toastr', toastr)
        .value("moment", moment);

    // put other stuff here as well ... 

})();