$(document).ready(function () {
    VendorRegistrationRequest();
});

function VendorRegistrationRequest() {
    if ($.fn.DataTable.isDataTable('#table1')) {
        $('#table1').DataTable().clear().destroy();
    }

    debugger;
    $.ajax({
        url: '/Admin/VendorRegistrationApprovalRequestJSON',
        type: 'Post',
        dataType: 'json',
        success: function (response) {
            debugger;
            if (response != null && response.length > 0) {
                var row = '';
                for (var i = 0; i < response.length; i++) {
                    row += `
                            <tr>
                                <td style="display:none;">${response[i].companyID}</td>
                                <td>${i + 1}</td>
                                <td>${response[i].vendorName}</td>
                                <td>${response[i].vendorAddress}</td>
                                <td>${response[i].vendorEmail}</td>
                                <td>${response[i].vendorPhone}</td>
                                <td>
                                <div class="d-flex justify-content-center gap-2">
                                    <button type="button" class="accept-btn btn btn-sm btn-success" onclick="RegisterVendor(${response[i].companyID})">Accept</button>
                                    <button type="button" class="delete-btn btn btn-sm btn-danger" onclick="DeleteVendor(${response[i].companyID})">Delete</button>
                                </div>
                            </td>
                            </tr>`;
                }
                $("#table1 tbody").html(row);

                $("#table1").DataTable({
                    "scrollX": true,
                    "paging": false,
                    "scrollCollapse": true,
                    "scrollY": '60vh'
                });
            } else {
                $("#table1 tbody").html('<tr><td colspan="6">No data found</td></tr>');
            }
        },
        error: function (xhr, status, error) {
            alert("Error occurred: " + error);
        }
    });
}

function RegisterVendor(companyId) {
    $.ajax({
        url: '/Admin/AccpetVendorRegistrationRequest',
        type: 'POST',
        dataType: 'Json',
        data: {
            tempCompanyId : companyId
        },
        success: function (response) {
            if (response.responseMessage!= null) {
                alert(response.responseMessage);
            }
            else {
                alert("Vendor Account Creation Failed")
            }
        },
        error: function (xhr, status, error) {
            alert("Error Occured");
        }
    })
}

function DeleteVendor(companyId) {
    $.ajax({
        url: '',
        type: 'POST',
        dataType: 'Json',
        data: {
            tempCompanyId: companyId
        },
        success: function (result) {
            if (result) {
                alert("Vendor Account Request Deleted Successfully");
            }
            else {
                alert("Vendor Account Request Deletion Failed");
            }
        },
        error: function (xhr, status, error) {
            alert("Error Occured");
        }
    })
}