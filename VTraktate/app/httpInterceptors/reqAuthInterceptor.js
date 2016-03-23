(() => {
    angular.module("app")
        .factory("ReqAuthInterceptor", [
            "Token", function(Token) {
                return {
                    'request': function(config) {
                        console.log("request interceptor:");
                        console.log(config)
                        var token = Token.get();
                        if (token) {
                            config.headers["Authorization"] = "Bearer " + token;
                        }
                        return config;
                    }
                }
            }
        ]);
})();