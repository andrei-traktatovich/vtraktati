(() => {
    window.createModule("blocks.error-handling")
        .factory("handleHttpError", handleHttpError);

    function handleHttpError($rootScope) {
        return (message) => (err) => {
            console.log("HTTP ERROR", err);
            $rootScope.$emit("http-error", {
                title: message || "Неизвестная ошибка",
                text: `${err.status} ${err.data.message || "Неизвестная ошибка сети"}`,
                error: err
            });
        };
    }
})();