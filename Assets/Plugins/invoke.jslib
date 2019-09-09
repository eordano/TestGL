mergeInto(LibraryManager.library, {
	hello: function() {
		console.log("hello world");
	},
    _UnityJS_HandleAwake: function _UnityJS_HandleAwake(activateCallback)
    {
        if (!window.callActivate) {
            window.callActivate = function _Activate(which, what) {
                return Runtime.dynCall('iii', activateCallback, [which, what]);
            }
        }
    }
})
