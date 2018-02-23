var coolBeans= {
	utilities: {},
	layout: {},
	page: {
		handlers: {
		},
		startUp: null
	},
	services: {},
	ui: {}
};

coolBeans.layout.startUp = function() {
	console.log("Startup firing!");
	console.debug("coolBeans.layout.startUp");
	
	if(coolBeans.page.startUp){
		console.debug("coolBeans.page.startUp");
		coolBeans.page.startUp();
	}
}

//Jquery Ajax Call: instead of PUT , theres GET, PUT, POST, DELETE
coolBeans.services.someName = coolBeans.services.someName || {};

coolBeans.services.someName.put = function (data, onSuccess, onError) {
    var url = "http://localhost:1552/api/someName";
    var settings = {
        cache: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(data),
        success: onSuccess,
        error: onerror,
        type: "PUT",
        xhrFields: { withCredentials: false }
    }
    $.ajax(url, settings)
}
