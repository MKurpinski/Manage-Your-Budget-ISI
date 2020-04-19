using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ManageYourBudget.Dtos.Auth.LoginDataProvider
{
    public class FacebookUserDto
    {
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [JsonProperty(PropertyName = "picture")]
        public PictureDto Picture { get; set; }
    }

    public class PictureDto
    {
        [JsonProperty(PropertyName = "data")]
        public DataDto PictureDataDto { get; set; }
    }

    public class DataDto
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
