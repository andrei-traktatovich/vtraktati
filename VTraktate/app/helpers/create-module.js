((global, isDebug) => {

    global.createModule = (name, deps) => {
        deps = deps || [];
        try {
            return angular.module(name);
        } catch (e) {
            if (isDebug)
                console.info(`createModule: module ${  name } is unavailable, creating it ... (${ e })`);

            return angular.module(name, deps);
        }
    }

})(window, true);