// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let TotalSale = 0;


const calculateproductCost = (e) => {
	e.preventDefault();
	e.stopPropagation();
	let previousCost = e.target.nextElementSibling.children[1].innerText
	let currentQtyValue = e.target.value;
	let productPrice = e.target.nextElementSibling.getAttribute("data-product-price")
	let newCost = (parseInt(currentQtyValue) * parseFloat(productPrice)).toFixed(2);
	e.target.nextElementSibling.children[1].innerText = newCost
	TotalSale -= +parseFloat(previousCost).toFixed(2);
	CalculateTotal(newCost);
}


const removeItem = (e) => {
	
	let costToRemove = e.target.previousElementSibling.innerText;
	let divToRemove = e.target.parentElement.parentElement.remove()
	if (document.querySelector("#sale_container").childElementCount === 0) {
		document.querySelector("#total_cost").innerText = 0;
		TotalSale = 0
		document.getElementById("total_cost_container").hidden = true;
		document.getElementById("pay-btn").hidden = true;
		document.getElementById("sale-header").hidden = true;
		return
	}
	CalculateTotal(-costToRemove)
}

const CalculateTotal = (addamount) => {
	TotalSale = +parseFloat(TotalSale).toFixed(2);
	addamount = +parseFloat(addamount).toFixed(2)
	TotalSale += addamount;
	document.getElementById("total_cost").innerText = parseFloat(TotalSale).toFixed(2)
}

const addtoSales = (event) => {
	event.preventDefault();
	if (!e) {
		var e = window.event
		e.cancelBubble = true;
		if (e.stopPropagation) e.stopPropagation();
	}

	
	const btn = event.target;
	const price = btn.previousElementSibling;
	const productNameContainer = price.previousElementSibling;
	const productName = productNameContainer.innerText;
	

	const imgContainer = productNameContainer.previousElementSibling;
	const productId = imgContainer.dataset.productId
	const divContainer = document.createElement("div");
	let img = document.createElement("img");
	img.src = imgContainer.src;
	img.dataset.name = productName;
	divContainer.append(img);
	event.target.parentElement.remove()

	const inputQty = document.createElement("input");
	const label = document.createElement("label");
	
	label.innerText = "QTY"
	inputQty.type = "number";
	inputQty.size = 10;
	inputQty.id = "numberinput";
	inputQty.name = "productqty";
	inputQty.value = 1;
	divContainer.append(label)
	divContainer.append(inputQty);
	inputQty.addEventListener("change", calculateproductCost);

	const totalPriceContainer = document.createElement("div");
	const spanLabel = document.createElement("span");
	const spanPrice = document.createElement("span");
	spanLabel.innerText = "Cost";

	let productCost = parseFloat(price.innerText);
	totalPriceContainer.setAttribute("data-product-price", productCost)
	totalPriceContainer.setAttribute("data-product-id", productId)
	spanPrice.innerText = productCost;
	totalPriceContainer.append(spanLabel);
	totalPriceContainer.append(spanPrice);


	const removeButton = document.createElement("button")
	removeButton.innerText = "Remove";
	removeButton.className = "btn btn-danger";
	totalPriceContainer.append(removeButton);
	removeButton.addEventListener("click",removeItem)
	divContainer.append(totalPriceContainer);
	document.getElementById("sale_container").append(divContainer);
	CalculateTotal(productCost);
	document.getElementById("total_cost_container").hidden = false;
	document.getElementById("pay-btn").hidden = false;
	document.getElementById("sale-header").hidden = false;


}

//const makeAjaxRequestonSubmit = async (oFormElement) => {
	
//	if (!e) {
//		var e = window.event
//		e.cancelBubble = true;
//		if (e.stopPropagation) e.stopPropagation();
//	}
//	const formData = new FormData(oFormElement);
//	let output = document.getElementById("product_suggestions")
//	try {
//		const response = await fetch(oFormElement.action, {
//			method: 'POST',
//			body: formData
//		});
//		const data = await response.json();

//		let dataObj = JSON.parse(data)
//		if (dataObj.length == 0) {
//			output.innerHTML = "<div class='no_search_results'>No products found</div>"
//		}

//		dataObj.forEach((property,index) => {
//			let divParent = document.createElement("div"+index);

//			let img = document.createElement("img");
//			img.src = property.image;
//			divParent.append(img)

//			let divName = document.createElement("div");
//			divName.innerText = property.Name;
//			divParent.append(divName)

//			let divPrice = document.createElement("div");
//			divPrice.innerText = property.Price;
//			divParent.append(divPrice)

//			let button = document.createElement("button")
//			button.innerText = "Add";
//			button.className = "btn_add_sales btn btn-primary"
//			divParent.append(button)
//			output.append(divParent)
//			button.addEventListener("click", addtoSales)
		
//		})

//		}

//		catch (error) {
//			console.error('Error:', error);
//		}
	
	
//}

const makeAjaxRequestonChange = async (event) => {
	let output = document.getElementById("product_suggestions");
	output.innerText = "";
	if (!e) {
		var e = window.event
		e.cancelBubble = true;
		if (e.stopPropagation) e.stopPropagation();
	}
	
	if (event.target.value.length < 3) {
		output.innerText = "Type more keywords";
		return false
	}
	
	document.getElementById("loading-products").removeAttribute("hidden");
	event.preventDefault();
	const oFormElement = document.getElementsByTagName("form")[0];
	const csrf = document.querySelector("input[name='__RequestVerificationToken']").value;
	const formData = new FormData();
	formData.append("hint", event.target.value);
	formData.append("__RequestVerificationToken", csrf);
	
	try {
		const response = await fetch(oFormElement.action,{
			method: 'POST',
			body: formData
		});
		const data = await response.json();

		let dataObj = JSON.parse(data)
		
		if (dataObj.length == 0) {
			output.innerHTML = "<div class='no_search_results'>No products found</div>"
			document.getElementById("loading-products").setAttribute("hidden", true);

		}

		dataObj.forEach(property => {
			let divParent = document.createElement("div");

			let img = document.createElement("img");
			img.src = property.Image;
			img.setAttribute("data-product-id", property.Id);
			divParent.append(img)

			let divName = document.createElement("div");
			divName.innerText = property.Name;
			divParent.append(divName)

			let divPrice = document.createElement("div");
			divPrice.innerText = property.Price;
			divParent.append(divPrice)

			let button = document.createElement("button")
			button.innerText = "Add"
			button.className = "btn_add_sales btn btn-primary"
			divParent.append(button)
			output.append(divParent)
			button.addEventListener("click", addtoSales);
			document.getElementById("loading-products").setAttribute("hidden", true);
			
		})

	}

	catch (error) {
		console.error('Error:', error);
	}
}


const submitSaleJson = async (event) => {
	let form = document.getElementById("savesaleform")
	dataToSend = saleToJson();
	dataToSend["completedOn"] = new Date().toISOString()
	const formData = new FormData();
	formData.append("data", JSON.stringify(dataToSend));
	const csrf = document.querySelector("input[name='__RequestVerificationToken']").value;
	formData.append("__RequestVerificationToken", csrf);
	
	try {
		const response = await fetch(form.action, {
			method: 'POST',
			body: formData
		});
		
		$("#savesaleform > button:nth-child(1)").hide()

	}
	catch (error) {
		console.error('Error:', error);
	};

}

function PrintContent() {
	var DocumentContainer = document.getElementById('modal_body_print');
	var WindowObject = window.open("", "PrintWindow",
		"width=750,height=650,top=50,left=50,toolbars=no,scrollbars=yes,status=no,resizable=yes");
	WindowObject.document.writeln(DocumentContainer.innerHTML);
	WindowObject.document.close();
	closeModal("staticBackdrop")
	setTimeout(function () {
		WindowObject.focus();
		WindowObject.print();
		WindowObject.close();
	}, 1000);
	closeModal("staticBackdropPrintModal")
}

const jsontoHtmlTable = () => {
	let data = saleToJson();
	const table = document.createElement("table");
	table.className = "table table-bordered";
	let rowHeaders = document.createElement("tr");
	let headers = `<th>Name</th><th>Qty</th><th>Price</th><th>Cost</th>`
	rowHeaders.innerHTML = headers;
	table.append(rowHeaders)

	data["items"].forEach((item) => {
		let row = document.createElement("tr");
		row.innerHTML = `
						<tr>
							<td>${item.name}</td>
							<td>${item.qty}</td>
							<td>${item.price}</td>
							<td>${item.cost}</td>
						</tr>`

		table.append(row)
	})
	let rowCost = document.createElement("tr");
	rowCost.innerHTML = `<td colspan=3>TotalCost</td><td>${data.totalCost}</td>`
	table.append(rowCost)
	document.getElementById("modal_body_print").append(table)
	return table

}


const saleToJson = () => {
	let dict = {};
	dict["items"] = []
	let totalCost = document.getElementById('total_cost_container').children[1].innerText;
	dict["totalCost"] = totalCost
	const childElementsArray = [...document.getElementById('sale_container').children]
	childElementsArray.forEach((el, index) => {
		let innerdict = {};
		const innerChildElementArray = [...el.children]
		innerChildElementArray.forEach((el) => {
			
			if (el.nodeName == "IMG") {
				let name = el.dataset.name;
				innerdict["name"] = name;	
					
			}
			if (el.nodeName == "INPUT") {
				let qty = el.value;
				innerdict["qty"] = qty;	
			}
			if (el.nodeName == "DIV") {
				let price = el.dataset.productPrice
				let cost = el.children[1].innerText
				let id = el.dataset.productId;
				innerdict["id"] = id;
				innerdict["price"] = price
				innerdict["cost"] = cost
			}	
		})
		dict["items"].push(innerdict)
	})
	return dict
}

//provide text id
const closeModal = (selector) => {
	var myModalEl = document.getElementById(selector);
	var modal = bootstrap.Modal.getInstance(myModalEl);
	modal.hide();
}

const showModal = (selector) => {
	var myModalEl = document.getElementById(selector);
	var modal = bootstrap.Modal.getInstance(myModalEl);
	modal.show();
}

// Builds the HTML Table out of myList.
function buildHtmlTable(selector,data) {
	var columns = addAllColumnHeaders(data, selector);

	for (var i = 0; i < data.length; i++) {
		var row$ = $('<tr/>');
		for (var colIndex = 0; colIndex < columns.length; colIndex++) {
			var cellValue = data[i][columns[colIndex]];
			if (cellValue == null)
				cellValue = "";
			row$.append($('<td/>').html(cellValue));
		}
		$(selector).append(row$);
	}
}

// Adds a header row to the table and returns the set of columns.
// Need to do union of keys from all records as some records may not contain
// all records.
function addAllColumnHeaders(data, selector) {
	var columnSet = [];
	var headerTr$ = $('<tr/>');

	for (var i = 0; i < data.length; i++) {
		var rowHash = data[i];
		for (var key in rowHash) {
			if ($.inArray(key, columnSet) == -1) {
				columnSet.push(key);
				headerTr$.append($('<th/>').html(key));
			}
		}
	}
	$(selector).append(headerTr$);

	return columnSet;
}
