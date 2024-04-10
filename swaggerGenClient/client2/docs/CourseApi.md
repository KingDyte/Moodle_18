# IO.Swagger.Api.CourseApi

All URIs are relative to *https://moodle_18.swagger.io/api/v3*

Method | HTTP request | Description
------------- | ------------- | -------------
[**4**](CourseApi.md#4) | **DELETE** /course | delete course
[**Addcourse**](CourseApi.md#addcourse) | **POST** /course | Add a new course
[**FindCourseByName**](CourseApi.md#findcoursebyname) | **GET** /course/findByName | Finds course by name
[**FindCourseByStatus**](CourseApi.md#findcoursebystatus) | **GET** /course/findByCode | Finds course by code
[**UpdateCourse**](CourseApi.md#updatecourse) | **PUT** /course | Update an existing course

<a name="4"></a>
# **4**
> void 4 ()

delete course

delete course

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class 4Example
    {
        public void main()
        {
            var apiInstance = new CourseApi();

            try
            {
                // delete course
                apiInstance.4();
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CourseApi.4: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="addcourse"></a>
# **Addcourse**
> Courses Addcourse (Courses body)

Add a new course

Add a new course

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AddcourseExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: moodle_auth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new CourseApi();
            var body = new Courses(); // Courses | Create course

            try
            {
                // Add a new course
                Courses result = apiInstance.Addcourse(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CourseApi.Addcourse: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**Courses**](Courses.md)| Create course | 

### Return type

[**Courses**](Courses.md)

### Authorization

[moodle_auth](../README.md#moodle_auth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml, application/x-www-form-urlencoded
 - **Accept**: application/json, application/xml

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="findcoursebyname"></a>
# **FindCourseByName**
> List<Courses> FindCourseByName (string tags = null)

Finds course by name

one name

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class FindCourseByNameExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: moodle_auth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new CourseApi();
            var tags = tags_example;  // string | Tags to filter by (optional)  (default to Matematika)

            try
            {
                // Finds course by name
                List&lt;Courses&gt; result = apiInstance.FindCourseByName(tags);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CourseApi.FindCourseByName: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **tags** | **string**| Tags to filter by | [optional] [default to Matematika]

### Return type

[**List<Courses>**](Courses.md)

### Authorization

[moodle_auth](../README.md#moodle_auth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="findcoursebystatus"></a>
# **FindCourseByStatus**
> List<Courses> FindCourseByStatus (string status = null)

Finds course by code

Multiple code values can be provided with comma separated strings

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class FindCourseByStatusExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: moodle_auth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new CourseApi();
            var status = status_example;  // string | Status values that need to be considered for filter (optional)  (default to available)

            try
            {
                // Finds course by code
                List&lt;Courses&gt; result = apiInstance.FindCourseByStatus(status);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CourseApi.FindCourseByStatus: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **status** | **string**| Status values that need to be considered for filter | [optional] [default to available]

### Return type

[**List<Courses>**](Courses.md)

### Authorization

[moodle_auth](../README.md#moodle_auth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="updatecourse"></a>
# **UpdateCourse**
> Courses UpdateCourse (Courses body)

Update an existing course

Update an existing course by Id

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class UpdateCourseExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: moodle_auth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new CourseApi();
            var body = new Courses(); // Courses | Update an existent course

            try
            {
                // Update an existing course
                Courses result = apiInstance.UpdateCourse(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CourseApi.UpdateCourse: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**Courses**](Courses.md)| Update an existent course | 

### Return type

[**Courses**](Courses.md)

### Authorization

[moodle_auth](../README.md#moodle_auth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml, application/x-www-form-urlencoded
 - **Accept**: application/json, application/xml

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
