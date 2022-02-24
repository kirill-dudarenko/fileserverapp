$(function () {

    //const baseUri = 'https://localhost:44345';
	const baseUri = 'http://localhost:8080';
  
  
	var actions = {
		'Copy' : OnCopy,	
		'Delete': OnDelete,
		'Rename': OnRename,
		'Create': OnCreate,
		'Download' : OnDownload,
		'Metadata': OnMetadata,
		'Upload': OnUpload
	};

	function OnCreate(id) {
		var tree = $.ui.fancytree.getTree('#treelist');
		var node = tree.getActiveNode();

		if (node.expanded === undefined) {
			node.setExpanded(true);
		}

		$.ajax({
			url: baseUri + '/resources/' + id + '/create',
			type: 'PUT',
			cache: false,
			processData: false,
			dataType: 'json',
			data: '"' + 'New Folder' + '"',
			contentType: 'text/json; charset=utf-8',
		})
		.done(function (data) {
			var tree = $.ui.fancytree.getTree('#treelist');
			var node = tree.getActiveNode();

			if (node) {

				node.addChildren({
					title: data.title,
					folder: data.folder,
					children: data.children,
					data: data,
					key: data.key
				});
			}
		})
		.error(function (response) {
			console.log(response);
		});
	}


	function OnCopy(id) {
		$.ajax({
            url: baseUri + '/resources/' + id + '/copy',
			type: 'GET',
            cache: false,
            dataType:'json'
		})
		.done(function(data){
			var tree = $.ui.fancytree.getTree('#treelist');
			var node = tree.getActiveNode();
			var parent = node.parent;
			
			if (parent) {

				parent.addChildren({
					title: data.title,
					folder: data.folder,
					children: data.children,
					data: data,
					key: data.key
				});
			}
		})
		.error(function (response) {
			console.log(response);
		});
	}

	function OnUpload(id) {
		var fileinput = $('<input/>', {
			type: 'file',
			style: 'display:none;',
			id: 'fileinput',
		});

		fileinput.on('change', function () {
			var formData = new FormData();
			formData.append('file', $('#fileinput')[0].files[0]);

			$.ajax({
				url: baseUri + '/resources/' + id + '/upload',
				type: 'POST',
				cache: false,
				contentType: false,
				processData: false,
				dataType: 'json',
				data: formData
			})
			.done(function (data) {
				var tree = $.ui.fancytree.getTree('#treelist');
				var node = tree.getActiveNode();

				if (node.expanded !== undefined) {
					node.addChildren({
						title: data.title,
						folder: data.folder,
						children: data.children,
						data: data,
						key: data.key
					});
				}

			})
			.error(function (response) {
				console.log(response)
			})
		});

		$('#operations').append(fileinput);

		$('#fileinput').trigger('click');
    }

	function OnDelete(id){
		$.ajax({
            url: baseUri + '/resources/' + id,
			type: 'DELETE',
            cache: false,
            dataType:'json'
		})
		.always(function(){
			var tree = $.ui.fancytree.getTree('#treelist');
			var node = tree.getActiveNode();
			
			if (node){
				node.remove();
			}
		});
	}
	
	function OnRename(id){
		var tree = $.ui.fancytree.getTree('#treelist');
		var node = tree.getActiveNode();
		
		node.editStart();
	}
	
	function DoRename(id, newname){
		var name = '"' + newname + '"';
		$.ajax({
            url: baseUri + '/resources/' + id + '/rename',
			type: 'PUT',
			dataType:'json',
            cache: false,
			contentType: 'text/json; charset=utf-8',
			processData: false,
			data: name
		})
		.done(function(data){		
			var tree = $.ui.fancytree.getTree('#treelist');
			var node = tree.getActiveNode();
			
			if (node){
				node.setTitle(data.title);
				node.key = data.key;
			}
		})
		.error(function (response) {
			console.log(response);
		});
	}
	
	function OnMetadata(id){
		$.ajax({
            url: baseUri + '/resources/' + id + '/metadata',
			type: 'GET',
            cache: false,
            dataType:'json'
		})
		.done(function(data){
			$('#metadata').text(data.MetadataDescription);
		})
		.error(function (response) {
			console.log(response);
		});
	}
	
	function OnDownload(id){
		var req = new XMLHttpRequest();
		var tree = $.ui.fancytree.getTree('#treelist');
		var node = tree.getActiveNode();
		
		req.open('GET', baseUri + '/resources/' + id + '/download', true);
		req.responseType = 'blob';
		req.onload = function (event) {
			var blob = req.response;
			var link = document.createElement('a');
			
			link.href = window.URL.createObjectURL(blob);
			link.download = node.title;
			link.click();
     };

     req.send();
	}

    $('#treelist').fancytree({
		extensions: ['edit'],
		edit: {
			save: function(event, data){
				var key = data.node.key,
				newName = data.input.val();
				
				DoRename(key, newName);
				return true;
			},
		},
        source:           
            $.ajax({
            url: baseUri + '/resources',
			type: 'GET',
            cache: false,
            dataType:'json'
        }),
		
        lazyLoad: function (_, data) {
            var node = data.node;

            data.result = {
                url: baseUri + '/resources/' + node.key + '/children',
                cache: false,
				dataType: 'json'
            };
        },
		
		activate: function(_, data){
			// A node was activated
			var node = data.node;

			$("#operations").empty();
					
			$.each(node.data.Actions, function(_,c){
				
				if (c === 'Scan')
					return;
				
				var opId = 'btn' + c;
				var btn = $('<button/>',{
					text: c,
					class: 'btn btn-primary',
					id: opId,
					value: node.key,
					click: function(e){
						e.preventDefault();
						
						var caption = this.textContent;
						
						if (caption == null)
							return;
											
						var action = actions[caption];
						
						if (action != null){
							action(this.value);
						}
					}
				});
				
				$("#operations").append(btn);
			})
		},
    })
});

