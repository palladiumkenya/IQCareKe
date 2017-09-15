var personAge = 0;

                /*----- make readonly by default ----- */
                $("#<%=ChildOrphan.ClientID%>").attr('disabled', 'disbaled');
                $("#<%=Inschool.ClientID%>").attr('disabled', 'disbaled');
                $("#<%=GurdianFNames.ClientID%>").attr('disabled', 'disbaled');
                $("#<%=GurdianMName.ClientID%>").attr('disabled', 'disbaled');
                $("#<%=GurdianLName.ClientID%>").attr('disabled', 'disbaled');
                $("#<%=GuardianGender.ClientID%>").attr('disabled', 'disbaled');

                $('#MyDateOfBirth').datepicker({
                        allowPastDates: true,
                        momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
                        restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
                });

                $('#MyDateOfBirth').on('changed.fu.datepicker dateClicked.fu.datepicker', function(event,date) {
                    var x = $('#MyDateOfBirth').datepicker('getDate');
                    $('#<%=personAge.ClientID%>').val(getAge(x));
                });

                $('#<%=countyId.ClientID%>').on("change", function() {
                     getSubcountyList(); /*call AJAX function */
                });

                $("#<%=SubcountyId.ClientID%>").on("change", function() {
                    getWardList();
                });
                
                /* Business Rules setup */
                $('#<%=personAge.ClientID%>').on("blur", function ()
                {
                    personAge = $(this).val();      
                    if (personAge >= 18)
                    {
                        $("#<%=ChildOrphan.ClientID%>").prop('disabled',true);
                        $("#<%=Inschool.ClientID%>").prop('disabled', true);
                        $("#<%=GurdianFNames.ClientID%>").prop('disabled', true);
                        $("#<%=GurdianMName.ClientID%>").prop('disabled', true);
                        $("#<%=GurdianLName.ClientID%>").prop('disabled', true);
                        $("#<%=GuardianGender.ClientID%>").prop('disabled',true);
                    } else {
                        $("#<%=ChildOrphan.ClientID%>").prop('disabled',false);
                        $("#<%=Inschool.ClientID%>").prop('disabled',false);
                        $("#<%=GurdianFNames.ClientID%>").prop('disabled',false);
                        $("#<%=GurdianMName.ClientID%>").prop('disabled',false);
                        $("#<%=GurdianLName.ClientID%>").prop('disabled',false);
                        $("#<%=GuardianGender.ClientID%>").prop('disabled',false);
                    }
                });

                $("#myWizard")
                    .on("actionclicked.fu.wizard", function(evt, data) {
                        var currentStep = data.step;
                        var nextStep = 0;
                        var previousStep = 0;
                        var totalError = 0;
                        var stepError = 0;
                        /*var form = $("form[name='form1']");*/
                           

                        if (data.direction === 'next')
                            nextStep=currentStep += 1;
                        else
                            previousStep=nextStep -= 1;
                        if (data.step === 1) {

                            /* add constraints based on age*/
                                          
                            if ($('#datastep1').parsley().validate()) {
                                addPerson();
                            } else {
                                stepError = $('.parsley-error').length === 0;
                                totalError += stepError;
                                evt.preventDefault();
                            }
                        }
                        else if (data.step === 2) {
                            if ($("#datastep2").parsley().validate()) {

                            } else {
                                stepError = $('.parsley-error').length === 0;
                                totalError += stepError;
                                evt.preventDefault();
                            }
                        }
                        else if (data.step === 3) {
                            if ($("#datastep3").parsley().validate()) {

                            } else {
                                stepError = $('.parsley-error').length === 0;
                                totalError += stepError;
                                evt.preventDefault();
                            }
                        }
                        else if (data.step===4) {
                            if ($("#datastep2").parsley().validate()) {

                            } else {
                                stepError = $('.parsley-error').length === 0;
                                totalError += stepError;
                                if (totalError > 0) {
                                    $('.bs-callout-danger').toggleClass('hidden', f);
                                }
                                evt.preventDefault();
                            }
                            //var ok4 = $('.parsley-error').length === 0;
                            //$('.bs-callout-info').toggleClass('hidden', !ok4);
                        }
                    })
                    .on("changed.fu.wizard",
                        function() {

                        })
                    .on('stepclicked.fu.wizard',
                        function() {

                        })
                    .on('finished.fu.wizard',
                        function(e) {

                        });

                /* calculate Person Age */
                function getAge(dateString) 
                {
                    var today = new Date();
                    var birthDate = new Date(dateString);
                    var age = today.getFullYear() - birthDate.getFullYear();
                    var m = today.getMonth() - birthDate.getMonth();
                    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) 
                    {
                        age--;
                    }
                    return age;
                }

                function getSubcountyList()
                {
                    var countyId = $("#<%=countyId.ClientID%>").find(":selected").text();
                    $.ajax({
                        type: "POST",
                        url: "../WebService/LookupService.asmx/GetLookupSubcountyList",
                        data: "{'county':'" + countyId + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var itemList = JSON.parse(response.d);
                            $("#<%=SubcountyId.ClientID%>").find('option').remove().end();
                            $("#<%=SubcountyId.ClientID%>").append('<option value="0">Select</option>');
                            $.each(itemList, function (index, itemList) {
                                $("#<%=SubcountyId.ClientID%>").append('<option value="' + itemList.subcountyId + '">' + itemList.SubcountyName + '</option>');
                            }); 
                        },
                        failure: function (msg) {
                            alert(msg);
                        }
                    });
                }

                function getWardList()
                {
                    var subcountyName = $("#<%=SubcountyId.ClientID%>").find(":selected").text();
                    $.ajax({
                        type: "POST",
                        url: "../WebService/LookupService.asmx/GetLookupWardList",
                        data: "{'subcounty':'" + subcountyName + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var itemList = JSON.parse(response.d);
                            $("#<%=WardId.ClientID%>").find('option').remove().end();

                            $("#<%=WardId.ClientID%>").append('<option value="0">Select</option>');
                            $.each(itemList, function (index, itemList) {
                                $("#<%=WardId.ClientID%>").append('<option value="' + itemList.WardId + '">' + itemList.WardName + '</option>');
                            }); 
                        },
                        failure: function (msg) {
                            alert(msg);
                        }
                    });     
                }

                function addPerson() {

                    var fname = $("#<%=personFname.ClientID%>").val();
                    var mname =  $("#<%=personMName.ClientID%>").val();
                    var lname =  $("#<%=personLName.ClientID%>").val();
                    var sex =  $("#<%=Gender.ClientID%>").find(":selected").val();
                    var natId = $("#<%=NationalId.ClientID%>").val();
                    //var userId = <%=UserId%>;

                    $.ajax({
                        type: "POST",
                        url: "../WebService/PersonService.asmx/AddPerson",
                        data: "{'firstname':'" + fname + "','middlename':'" + mname + "','lastname':'" + lname + "','gender':" + sex + ",'nationalId':'" + natId + "','userId':'" + userId + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            // alert(response.d);
                            generate('success', response.d);
                        },
                        failure: function (msg) {
                            // alert(msg);
                            generate('error', msg);
                        }
                    });
                }

                function generate(type, text) {

                    var n = noty({
                        text: text,
                        type: type,
                        dismissQueue: true,
                        progressBar: true,
                        timeout: 5000,
                        layout: 'top',
                        closeWith: ['click'],
                        theme: 'relax',
                        maxVisible: 10,
                        animation: {
                            open: 'animated bounceInLeft',
                            close: 'animated bounceOutLeft',
                            easing: 'swing',
                            speed: 500
                        }
                    });
                    console.log('html: ' + n.options.id);
                    return n;
                }