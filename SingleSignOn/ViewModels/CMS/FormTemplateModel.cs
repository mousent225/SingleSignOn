using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SingleSignOn.ViewModels
{
    public class FormTemplateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Title")]
        public string Subject { get; set; }

        [Display(Name = "Description")]
        [StringLength(4000, ErrorMessage = "Maximum {2} characters exceeded") ]
        public string Descript { get; set; }
        
        public Guid Category { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        
        public string Writer { get; set; }
        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Writer")]
        public string WriterName { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Write Date")]
        public DateTime WriteDate { get; set; }

        [Display(Name = "Attachment File")]
        public bool AttachFile { get; set; }

        [Display(Name = "Modify Date")]
        public DateTime UpdateDate { get; set; }

        [Display(Name = "Modify User")]
        public string UpdateUid { get; set; }

        [Display(Name = "Modify User")]
        public string UpdateName { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        public bool IsContinue { get; set; }
        public bool? IsSubmit { get; set; }
        public bool? IsApprove { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string ApproveName { get; set; }
        public string Reason { get; set; }
        public DateTime? SubmitDate { get; set; }
        public IEnumerable<AttachFileModel> ListAttachFile { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int NumRead { get; set; }
        public bool Pin { get; set; }
        public string Status { get; set; }

    }
}