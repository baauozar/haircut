$(function () {
    // Function to load Beauty Categories dynamically into the dropdown
    function loadBeautyCategories() {
        $.ajax({
            url: '/Admin/HairCutSupServices/GetCategories', // API endpoint
            type: 'GET',
            success: function (categories) {
                const dropdown = $('#ServiceCategorydata'); // Target the dropdown by ID
                dropdown.empty(); // Clear existing options
                dropdown.append('<option value="">Please select</option>'); // Add default option

                // Append each category dynamically
                categories.forEach(function (category) {
                    dropdown.append($('<option>', {
                        value: category.id,
                        text: category.name
                    }));
                });

                // Refresh dropdown if using Bootstrap Select
                if ($.fn.selectpicker) {
                    dropdown.selectpicker('refresh'); // Refresh dropdown
                }
            },
            error: function (xhr, status, error) {

                alert('Failed to load Beauty Categories.');
            }
        });
    }

    // Load categories when the modal is shown
    $('.bd-example-modal-lg').on('shown.bs.modal', function () {
        loadBeautyCategories();
    });

    // Validate and submit the form
    $('#btnSubmitSupService').on('click', function (e) {
        e.preventDefault();
        let isValid = true;

        // Validate all required fields (input and select)
        $('#SupServiceAdd input[required], #SupServiceAdd select[required]').each(function () {
            const value = $(this).val().trim();
            if (value === '' || value === 'Please select') {
                isValid = false;
                $(this).addClass('is-invalid');
                $(this).closest('.mb-3').find('.invalid-feedback').show(); // Targets the nearest feedback div
            } else {
                $(this).removeClass('is-invalid');
                $(this).closest('.mb-3').find('.invalid-feedback').hide(); // Hides the feedback div when valid
            }
        });

        if (!isValid) {
            toastr.error('Please fill out all required fields.');
            return;
        }

        // Serialize form and submit via AJAX
        const formData = $('#SupServiceAdd').serialize();
        $.ajax({
            url: '/Admin/HairCutSupServices/Create',
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    $('.bd-example-modal-lg').modal('hide'); // Hide modal on success

                    location.reload(); // Reload page to reflect changes
                } else {
                    toastr.error(response.message || 'There was an error.');
                }
            },
            error: function (xhr, status, error) {

                toastr.error('An unexpected error occurred.');
            }
        });
    });

    // Reset the modal form on close




    $(document).on('click', '.DeleteHaricutSupService', function (e) {
        e.preventDefault();
        var Id = $(this).data('id');  // seçili kapasitenin ID'si alınıyor

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
                    url: '/Admin/HairCutSupServices/Delete',
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



    $(".btnEdit").on('click', function () {
        const id = $(this).data('id');
        const name = $(this).data('name');
        const description = $(this).data('description');
        const categoryid = $(this).data('categoryid'); // The selected category ID

        // Populate form fields
        $('#editSepServiceId').val(id).data('original', id);
        $('#editName').val(name).data('original', name);
        $('#editDescription').val(description).data('original', description);

        // Fetch and populate the categories in the dropdown
        $.ajax({
            url: '/Admin/HairCutSupServices/GetCategories',
            type: 'GET',
            success: function (data) {
                let options = '<option value="">Please select</option>';
                data.forEach(item => {
                    options += `<option value="${item.id}" ${item.id == categoryid ? 'selected' : ''}>${item.name}</option>`;
                });

                // Populate the dropdown and set the original value for change detection
                const dropdown = $('#editServiceCategorydata');
                dropdown.html(options).data('original', categoryid);

                // Refresh Bootstrap Select plugin (if used)
                if ($.fn.selectpicker) {
                    dropdown.selectpicker('refresh');
                }
            },
            error: function () {
                alert('Failed to load categories.');
            }
        });
    });

    $("#btnEditSepService").on('click', function () {
        let isValid = true; // Validation flag
        let isChanged = false; // Change detection flag

        // Validate and check for changes
        $('#SepServiceEdit input[required], #SepServiceEdit select[required]').each(function () {
            const originalValue = $(this).data('original');
            const currentValue = $(this).val();

            // Validation logic
            if (currentValue === '' || currentValue === 'Please select') {
                isValid = false;
                $(this).addClass('is-invalid');
                $(this).closest('.form-group, .col-lg-6').find('.invalid-feedback').show();
            } else {
                $(this).removeClass('is-invalid');
                $(this).closest('.form-group, .col-lg-6').find('.invalid-feedback').hide();
            }

            // Change detection logic
            if (originalValue !== undefined && originalValue.toString() !== currentValue.toString()) {
                isChanged = true;
            }
        });

        // Stop if validation fails
        if (!isValid) {
            toastr.error('Please fill out all required fields.');
            return;
        }

        // Stop if no changes are made
        if (!isChanged) {
            Swal.fire('No changes made', 'Please modify at least one field before submitting.', 'info');
            return;
        }

        // Submit form via AJAX
        const formData = $('#SepServiceEdit').serialize();
        $.ajax({
            type: 'POST',
            url: '/Admin/HairCutSupServices/Edit',
            data: formData,
            success: function (response) {
                if (response.success) {
                    $('.bd-example-modal-lg-edit').modal('hide'); // Close modal
                    location.reload(); // Reload page to reflect changes
                } else {
                    Swal.fire('Error', response.message || 'There was an error.', 'error');
                }
            },
            error: function (xhr) {
                console.error('Error:', xhr.responseText);
                Swal.fire('Error', 'An unexpected error occurred.', 'error');
            }
        });
    });

    $('.bd-example-modal-lg').on('hidden.bs.modal', function () {
        // Reset the form fields
        $('#SupServiceAdd')[0].reset();

        // Remove validation styles for inputs and selects
        $('#SupServiceAdd input, #SupServiceAdd select').removeClass('is-invalid');

        // Hide all invalid-feedback messages
        $('#SupServiceAdd .invalid-feedback').hide();

        // Optionally, clear toastr notifications
        toastr.clear();
    });
    $('.bd-example-modal-lg').on('hidden.bs.modal', function () {
        // Reset the form fields
        $('#SepServiceEdit')[0].reset();

        // Remove validation styles for inputs and selects
        $('#SepServiceEdit input, #SepServiceEdit select').removeClass('is-invalid');

        // Hide all invalid-feedback messages
        $('#SepServiceEdit .invalid-feedback').hide();

        // Optionally, clear toastr notifications
        toastr.clear();
    });
});
