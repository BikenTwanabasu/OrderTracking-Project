$(document).ready(function () {
    AgentRegistrationRequest();
});

function AgentRegistrationRequest() {
    if ($.fn.DataTable.isDataTable('#table1')) {
        $('#table1').DataTable().clear().destroy();
    }
    $.ajax({
        url: '/Admin/AgentRegistrationApprovalRequestJSON',
        type: 'Post',
        dataType: 'json',
        success: function (response) {
            debugger;
            if (response != null && response.length > 0) {
                var row = '';
                for (var i = 0; i < response.length; i++) {
                    row += `
                            <tr>
                                <td style="display:none;">${response[i].agentId}</td>
                                <td>${i + 1}</td>
                                <td>${response[i].agentName}</td>
                                <td>${response[i].agentAddress}</td>
                                <td>${response[i].agentEmail}</td>
                                <td>${response[i].agentPhone}</td>
                                <td>
                                <div class="d-flex justify-content-center gap-2">
                                    <button type="button" class="accept-btn btn btn-sm btn-success" onclick="RegisterAgent(${response[i].agentId})">Accept</button>
                                    <button type="button" class="delete-btn btn btn-sm btn-danger" onclick="DeleteAgent(${response[i].agentId})">Delete</button>
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

function RegisterAgent(agentId) {
    $.ajax({
        url: '/Admin/AcceptAgentRegistrationRequest',
        type: 'POST',
        dataType: 'Json',
        data: {
            tempAgentId: agentId
        },
        success: function (response) {
            if (response.responseMessage != null) {
                alert(response.responseMessage);
                debugger;
                window.location.href = '/Admin/AgentRegistrationApprovalRequest';
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

function DeleteAgent(agentId) {
    $.ajax({
        url: '',
        type: 'POST',
        dataType: 'Json',
        data: {
            tempAgentId: agentId
        },
        success: function (result) {
            if (result) {
                alert("Vendor Account Request Deleted Successfully");
                window.location.href = '/Admin/AgentRegistrationApprovalRequest';
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