
toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

var loadCommonModal = function (url) {
    $("#SmallModalDiv").load(url, function () {
        $("#SmallModal").modal("show");
        $("#Name").focus();
    });
};

var loadMediumModal = function (url) {
    $("#MediumModalDiv").load(url, function () {
        $("#MediumModal").modal("show");
        $("#Name").focus();
    });
};

var loadBigModal = function (url) {
    $("#BigModalDiv").load(url, function () {
        $("#BigModal").modal("show");
        $('#Name').focus();
    });
};

var loadExtraBigModal = function (url) {
    $("#ExtraBigModalDiv").load(url, function () {
        $("#ExtraBigModal").modal("show");
    });
};

var loadPrintModal = function (url) {
    $("#PrintModalDiv").load(url, function () {
        $("#PrintModal").modal("show");
    });
};

var loadImageViewModal = function () {
    $("#ImageViewModal").modal("show");
};



var AddCustomer = function (id) {
    $('#titleMediumModal').html("Add Customer");
    loadMediumModal("/CustomerInfo/AddEdit?id=" + id);
};

var AddSupplier = function (id) {
    $('#titleMediumModal').html("Add Supplier");
    loadMediumModal("/Supplier/AddEdit?id=" + id);
};

var AddCategories = function (id) {
    $('#titleMediumModal').html("Add Categories");
    loadMediumModal("/Categories/AddEdit?id=" + id);
};

var AddUnitsofMeasure = function (id) {
    $('#titleMediumModal').html("Add Units of Measure");
    loadMediumModal("/UnitsofMeasure/AddEdit?id=" + id);
};


var SearchInHTMLTable = function () {
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

var SearchByInHTMLTable = function (TableName) {
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();

    tr = TableName.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

var FieldValidation = function (FieldName) {
    var _FieldName = $(FieldName).val();
    if (_FieldName == "" || _FieldName == null) {
        return false;
    }
    return true;
};

var FieldValidationAlert = function (FieldName, Message, icontype) {
    Swal.fire({
        title: Message,
        icon: icontype,
        onAfterClose: () => {
            $(FieldName).focus();
        }
    });
}

var SwalSimpleAlert = function (Message, icontype) {
    Swal.fire({
        title: Message,
        icon: icontype
    });
}

var FieldIdNullCheck = function (FieldId) {
    if (FieldId === null || FieldId === '')
        FieldId = 0;
    return FieldId;
}

var ViewImage = function (imageURL, Title) {
    $('#titleImageViewModal').html(Title);
    $("#UserImage").attr("src", imageURL);
    $("#ImageViewModal").modal("show");
};

var ViewImageByURLOnly = function (imageURL) {
    $('#titleImageViewModal').html('Asset Image');
    $("#UserImage").attr("src", imageURL);
    $("#ImageViewModal").modal("show");
};

var activaTab = function (tab) {
    $('.nav-tabs a[href="#' + tab + '"]').tab('show');
};

var BacktoPreviousPage = function () {
    window.history.back();
}

var printDiv = function (divName, RemoveCssClass) {
    $("table").removeClass(RemoveCssClass);
    var printContents = document.getElementById("printableArea").innerHTML;
    var originalContents = document.body.innerHTML;
    document.body.innerHTML = printContents;
    window.print();
    document.body.innerHTML = originalContents;
}

var printBarcodeDiv = function (printableBarcodeAreaId) {
    var printContents = document.getElementById(printableBarcodeAreaId).innerHTML;
    var originalContents = document.body.innerHTML;
    document.body.innerHTML = printContents;
    window.print();
    document.body.innerHTML = originalContents;
}

var printBarcodeDivThurmal = function (Barcode) {
    document.getElementById("imgSingleBarcode").classList.add('imgCustomThurmal');
    $("#imgSingleBarcode").attr("src", Barcode);

    var printContents = document.getElementById('divSingleBarcodeDIV').innerHTML;
    var originalContents = document.body.innerHTML;
    document.body.innerHTML = printContents;
    window.print();
    document.body.innerHTML = originalContents;

    $("#imgSingleBarcode").attr("src", '');
    document.getElementById("imgSingleBarcode").classList.remove('imgCustomThurmal');
}
function DisplayNotificationCount(count) {
    document.getElementById("lbNotificationCount").innerHTML = count;

}
function DisplayGeneralNotification(message, title) {


    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            timeOut: 4000
        };
        toastr.info(message, title);

    }, 1300);
}

function DisplayPersonalNotification(message, title) {
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            timeOut: 4000
        };
        toastr.success(message, title);

    }, 1300);
}
