$(function () {
  
    $('#btnSubmitCompany').on('click', function (e) {

        e.preventDefault();
        var isValid = true; // Flag to track validation
        $('#CompanyAdd input[required]').each(function () {
            if ($(this).val().trim() === '') { // Check if the field is empty
                isValid = false; // Set flag to false
                $(this).addClass('is-invalid'); // Add an invalid class for styling
                $(this).next('.invalid-feedback').show(); // Show the invalid feedback
            } else {
                $(this).removeClass('is-invalid'); // Remove invalid class if valid
                $(this).next('.invalid-feedback').hide(); // Hide the invalid feedback
            }
        });

        if (!isValid) {
            toastr.error('Please fill out all required fields.');
            return; // Exit the function if validation fails
        }

        // Serialize the form data
        var formData = $('#CompanyAdd').serialize();

        $.ajax({
            type: 'POST',
            url: '/Admin/Company/Create',
            data: formData,
            success: function (response) {
                if (response.success) {
                    $('.bd-example-modal-lg').modal('hide');

                    location.reload();

                    toastr.success(response.message);

                } else {
                    toastr.error("There was an error");
                }
            },
            error: function (response) {
                console.log("Error: " + response);
            }
        });

    });
    $(".btnEdit").on('click', function () {
        var id = $(this).data('id');
        var smalltitle = $(this).data('smalltitle');
        var bigtitle = $(this).data('bigtitle');
        var baakgroundtitle = $(this).data('baakgroundtitle');
        var section = $(this).data('section');
        var signature = $(this).data('signature');
     
        $('#editCompanyId').val(id).data('original', id);;
        $('#editsmallTitle').val(smalltitle).data('original', smalltitle);
        $('#editbigTitle').val(bigtitle).data('original', bigtitle);;
        $('#editBackgroundTitle').val(baakgroundtitle).data('original', baakgroundtitle);;
        $('#editSection').val(section).data('original', section);;
        $('#editSignature').val(signature).data('original', signature);;
       
    });
    $("#btnEditCompany").on('click', function () {
        var isValid = true; // Flag to track validation
        $('#CompanyEdit input[required]').each(function () {
            if ($(this).val().trim() === '') { // Check if the field is empty
                isValid = false; // Set flag to false
                $(this).addClass('is-invalid'); // Add an invalid class for styling
                $(this).next('.invalid-feedback').show(); // Show the invalid feedback
            } else {
                $(this).removeClass('is-invalid'); // Remove invalid class if valid
                $(this).next('.invalid-feedback').hide(); // Hide the invalid feedback
            }
        });
        if (!isValid) {
            toastr.error('Please fill out all required fields.');
            return; // Exit the function if validation fails
        }
        // Check if at least one field value has changed
        var isChanged = false;
        $('#btnEditCompany input[required]').each(function () {
            var originalValue = $(this).data('original');
            var currentValue = $(this).val();

            // If at least one field is different
            if (originalValue !== currentValue) {
                isChanged = true;
                return false; // Exit loop once a change is found
            }
        });

        if (!isChanged) {
            // Alert the user if no changes were made
            Swal.fire('No changes made', 'Please modify at least one field before submitting.', 'info');
            return; // Stop form submission
        }
        var formData = $('#CompanyEdit').serialize();  // Serialize the form data

        // Making an AJAX request to submit the edited data
        $.ajax({
            type: 'POST',
            url: '/Admin/Company/Edit',
            data: formData,
            success: function (response) {
                // Handle the response here
                if (response.success) {
                    toastr.success(response.message);
                    location.reload();  // Reload the page to see updated data
                } else {
                    toastr.error('Error updating the faq');
                }
            },
            error: function (error) {
                console.log('Error updating the brand', error);
            }
        });
    });

    
    $(document).on('click', '.DeleteCompany', function (e) {
        e.preventDefault();
        var Id = $(this).data('id');  // seçili kapasitenin ID'si alınıyor
        console.log('Selected ID:', Id);
        // kullanıcıya silme işlemi için onay mesajı gösteriliyor
        Swal.fire({
            title: 'Emin misiniz?',
            text: "Bu işlemi geri alamayacaksınız!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet, sil!'
        }).then((result) => {
            if (result.isConfirmed) {
                // onay alındıysa AJAX ile silme isteği gönderiliyor
                $.ajax({
                    type: 'POST',
                    url: '/Admin/Company/Delete',
                    data: { id: Id },
                    success: function (response) {
                        if (response.success) {
                            toastr.success(response.message);  // başarı mesajı gösteriliyor
                            location.reload();  // başarı durumunda sayfa yenileniyor
                        } else {
                            toastr.error('Hata', 'Öğe silinemedi.', 'error');  // hata mesajı gösteriliyor
                        }
                    },
                    error: function (xhr, status, error) {

                        console.log('Hata', 'Öğeyi silerken bir hata oluştu.', 'error');  // genel hata mesajı gösteriliyor
                    }
                });
            }
        });
    });
    // Reset the modal when it is closed
    $('.bd-example-modal-lg').on('hidden.bs.modal', function () {
        // Reset the form fields
        $('#CompanyAdd')[0].reset();

        // Remove validation styles
        $('#CompanyAdd input').removeClass('is-invalid');
        $('#CompanyAdd .invalid-feedback').hide();

        // Optionally, clear toastr notifications
        toastr.clear();
    });
    $('.bd-example-modal-lg-edit').on('hidden.bs.modal', function () {
        // Reset the form fields
        $('#CompanyEdit')[0].reset();

        // Remove validation styles
        $('#CompanyEdit input').removeClass('is-invalid');
        $('#CompanyEdit .invalid-feedback').hide();

        // Optionally, clear toastr notifications
        toastr.clear();
    });
})