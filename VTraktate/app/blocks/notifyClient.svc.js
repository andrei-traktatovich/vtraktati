(() => {
    window.createModule("blocks.nofifyClient")
        .factory("notifyClient", appMessage)
        .value("DEBUG", true)
        .value("localStorage", localStorage);

    function appMessage(toastr, DEBUG, now) {
        return {
            error,
            info,
            success,
            warning
        };

        function error(title, message, err) {
            
            showMessage("error", title, message, err);

        }

        function info(title, message, err) {
            showMessage("info", title, message, err);
        }

        function success(title, message, err) {
            showMessage("success", title, message, err);
        }

        function warning(title, message, err) {
            showMessage("warning", title, message, err);
        }

        function showMessage(type, title, message, err) {
            toastr[type](title, message);
            if (DEBUG) {
                console.log(title, message, err);
                if (type === "error")
                    localStorage.setItem(`ERR${title} : ${now()}`, JSON.stringify({
                        title,
                        message,
                        err
                    }));
            }
        }
    }

})();