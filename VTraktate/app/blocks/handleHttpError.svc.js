(() => {
    window.createModule("blocks.error-handling")
        .factory("handleHttpError", handleHttpError);

    function handleHttpError($rootScope) {
        return (message) => (err) => {
            $rootScope.$emit("http-error", {
                title: message || "Неизвестная ошибка",
                text: `${err.status} ${err.data.error || "Неизвестная ошибка сети"}`,
                error: err
            });
        };
    }
})();