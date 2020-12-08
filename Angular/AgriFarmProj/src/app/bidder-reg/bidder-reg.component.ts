import { Component, OnInit } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms'; 
import {HttpClient, HttpHeaders} from '@angular/common/http';

import { BidderData } from '../models/Bidderdata';
import { BidderService } from '../services/bidderregservice';


@Component({
  selector: 'app-bidder-reg',
  templateUrl: './bidder-reg.component.html',
  styleUrls: ['./bidder-reg.component.css']
})
export class BidderRegComponent implements OnInit {
bidderdetails : BidderData

  imageUrl:string= "/assets/images/bidder.jpg";
  fileToUpload:File=null;

  //farmservice : RequestService;

  imageUrl1:string= "/assets/images/bidder.jpg";
  fileToUpload1:File=null;

  constructor(private http:HttpClient, private bidderservice : BidderService) {
    this.bidderdetails = {
      BidderName: "",
      BidderCity:"",
      BidderAadhar: "",
      BidderAddress: "",
      BidderContactNo: "",
      BidderEmail:"",
      BidderPassword:"",
      BidderPincocde:"",
      BidderState:"",
      BidderTradeLicense:"",
      BidderConfirmPassword: "",

      AccountNo:"",
      IFSC_Code:""
    }
  }

  handleFileInput(file: FileList) {
    this.fileToUpload=file.item(0);

    //shwing image preview 
    var reader = new FileReader();
    reader.onload=(event:any)=>{
      this.imageUrl=event.target.result;
    }
    reader.readAsDataURL(this.fileToUpload);
  }

  handleFileInput1(file: FileList) {
  this.fileToUpload1=file.item(0);

  //shwing image preview 
  var reader1 = new FileReader();
  reader1.onload=(event:any)=>{
    this.imageUrl1=event.target.result;
  }
  reader1.readAsDataURL(this.fileToUpload1);
}

OnSubmit(BidderName,BidderEmail,BidderContactNo,BidderAddress, BidderCity
  ,BidderState,BidderPincocde, BidderAadhar,BidderTradeLicense,BidderPassword,
  AccountNo,IFSC_Code){

  this.bidderservice.postFile(BidderName.value,BidderEmail.value,BidderContactNo.value,BidderAddress.value, BidderCity.value
    ,BidderState.value,BidderPincocde.value,this.fileToUpload,this.fileToUpload1, BidderPassword.value,
    AccountNo.value,IFSC_Code.value
    ).subscribe(
    data =>{
      alert("Data Added Successfully");
      console.log('done');
      BidderName.value = "";
      BidderEmail.value = "";
      BidderContactNo.value = "";
      BidderAddress.value = "";
      BidderCity.value = "";
      BidderState.value = "";
      BidderPincocde.value = "";
      BidderPassword.value = "";
      AccountNo.value = "";
      IFSC_Code.value = "";
      BidderAadhar.value ="";
      BidderTradeLicense.value="";
      
      this.imageUrl = "/assets/images/bidder.jpg";
      this.imageUrl1 = "/assets/images/bidder.jpg";
    }
  );
 }

    ngOnInit() :void{
    }


  /*
    OnBidDetails(bidderform:NgForm){
      console.log(bidderform.value);
      const httpheader={headers:new HttpHeaders({'Content-Type':'application/json'})};
      this.http.post('https://localhost:44322/api/bidderreg/',JSON.stringify(bidderform.value),httpheader)
      .subscribe((data)=>
      {console.log(data);
      })
    }  
  */
   }

