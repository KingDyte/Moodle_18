# IO.Swagger.Api.UserApi

All URIs are relative to *https://moodle_18.swagger.io/api/v3*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CourseGet**](UserApi.md#courseget) | **GET** /course | listEnrolledStudents

<a name="courseget"></a>
# **CourseGet**
> User CourseGet ()

listEnrolledStudents

listEnrolledStudents

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CourseGetExample
    {
        public void main()
        {
            var apiInstance = new UserApi();

            try
            {
                // listEnrolledStudents
                User result = apiInstance.CourseGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling UserApi.CourseGet: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**User**](User.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
