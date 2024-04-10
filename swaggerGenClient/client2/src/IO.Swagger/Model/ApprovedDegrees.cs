/* 
 * Swagger Moodle - OpenAPI 3.0
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0.11
 * Contact: apiteam@swagger.io
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = IO.Swagger.Client.SwaggerDateConverter;
namespace IO.Swagger.Model
{
    /// <summary>
    /// ApprovedDegrees
    /// </summary>
    [DataContract]
        public partial class ApprovedDegrees :  IEquatable<ApprovedDegrees>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApprovedDegrees" /> class.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="courseId">courseId.</param>
        /// <param name="degreeId">degreeId.</param>
        public ApprovedDegrees(long? id = default(long?), long? courseId = default(long?), long? degreeId = default(long?))
        {
            this.Id = id;
            this.CourseId = courseId;
            this.DegreeId = degreeId;
        }
        
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or Sets CourseId
        /// </summary>
        [DataMember(Name="course_id", EmitDefaultValue=false)]
        public long? CourseId { get; set; }

        /// <summary>
        /// Gets or Sets DegreeId
        /// </summary>
        [DataMember(Name="degree_id", EmitDefaultValue=false)]
        public long? DegreeId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ApprovedDegrees {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  CourseId: ").Append(CourseId).Append("\n");
            sb.Append("  DegreeId: ").Append(DegreeId).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as ApprovedDegrees);
        }

        /// <summary>
        /// Returns true if ApprovedDegrees instances are equal
        /// </summary>
        /// <param name="input">Instance of ApprovedDegrees to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ApprovedDegrees input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.CourseId == input.CourseId ||
                    (this.CourseId != null &&
                    this.CourseId.Equals(input.CourseId))
                ) && 
                (
                    this.DegreeId == input.DegreeId ||
                    (this.DegreeId != null &&
                    this.DegreeId.Equals(input.DegreeId))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.CourseId != null)
                    hashCode = hashCode * 59 + this.CourseId.GetHashCode();
                if (this.DegreeId != null)
                    hashCode = hashCode * 59 + this.DegreeId.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
