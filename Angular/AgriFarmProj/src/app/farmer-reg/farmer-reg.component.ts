import { Component, OnInit } from '@angular/core';

import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms'; 
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Farmerdata } from '../models/Farmerdata';
import { RequestService } from '../services/farmerregservice';

@Component({
  selector: 'app-farmer-reg',
  templateUrl: './farmer-reg.component.html',
  styleUrls: ['./farmer-reg.component.css']
})
export class FarmerRegComponent implements OnInit {
  farmerdetails:Farmerdata
  imageUrl:string= "/assets/images/farmer.jpg";
  fileToUpload:File=null;

  //farmservice : RequestService;

  imageUrl1:string= "/assets/images/farmer.jpg";
  fileToUpload1:File=null;
  
  alert: boolean= false

  //farmerdetails:Farmer
  //farmerlanddetails : FarmerLand;
  //farmerbankdetails : FarmerBank;
  constructor(private http:HttpClient , private farmservice : RequestService ) {
    this.farmerdetails = {
      FarmerName: "",
      FarmerAadhar:"",
      FarmerAddress:"",
      FarmerCity:"",
      FarmerEmail:"",
      FarmerContactNo:"",
      FarmerState:"",
      FarmerCertificate:"",
      FarmerPassword:"",
      FarmerConfirmPassword:"",
      FarmerPincocde:"",

      FarmerLandAddress:"",
      FarmerLandArea:"",
      FarmerLandPincode:"",
      AccountNo:"",
      IFSC_Code:""
    }
    /*
    this.farmerlanddetails = {
      FarmerLandAddress:"",
      FarmerLandArea:"",
      FarmerLandPincode:""
    }

    this.farmerbankdetails = {
      AccountNo:"",
      IFSC_Code:""

    }*/

   }

  ngOnInit() :void{
    
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

OnSubmit(FarmerName,FarmerEmail,FarmerContactNo,FarmerAddress, FarmerCity
  ,FarmerState,FarmerPincocde,A,B, FarmerPassword,
  FarmerLandArea,FarmerLandAddress,FarmerLandPincode,AccountNo,IFSC_Code){

  this.farmservice.postFile(FarmerName.value,FarmerEmail.value,FarmerContactNo.value,FarmerAddress.value, FarmerCity.value
    ,FarmerState.value,FarmerPincocde.value,this.fileToUpload,this.fileToUpload1, FarmerPassword.value,
    FarmerLandArea.value,FarmerLandAddress.value,FarmerLandPincode.value,AccountNo.value,IFSC_Code.value,
    ).subscribe(
    data =>{
      alert("Data Added Successfully");
      console.log('done');
      FarmerName.value = "";
      FarmerEmail.value = "";
      FarmerContactNo.value = "";
      FarmerAddress.value = "";
      FarmerCity.value = "";
      FarmerLandArea.value = "";
      FarmerLandAddress.value = "";
      FarmerLandPincode.value = "";
      AccountNo.value = "";
      IFSC_Code.value = "";
      A.value ="";
      FarmerPassword.value = "",
      B.value="";
      
      this.imageUrl = "/assets/images/farmer.jpg";
      this.imageUrl1 = "/assets/images/farmer.jpg";
      
    }
  );
 }

/*  OnDetails(farmerform:NgForm){
    console.log(farmerform.value);
    const httpheader={headers:new HttpHeaders({'Content-Type':'application/json'})};
    this.http.post('https://localhost:44322/api/registration/',JSON.stringify(farmerform.value),httpheader)
    .subscribe((data)=>
    {console.log(data);
      this.alert= true;  
    })
  }
  
  closeAlert(){
    this.alert=false;
  }*/
  
  /*
  OnLandDetails(landform:NgForm){
    console.log(landform.value);
    const httpheader={headers:new HttpHeaders({'Content-Type':'application/json'})};
    this.http.post("https://localhost:44329/api/contactus/",JSON.stringify(landform.value),httpheader)
    .subscribe((data)=>
    {console.log(data);
    })
  }

  OnBankDetails(bankform:NgForm){
    console.log(bankform.value);
    const httpheader={headers:new HttpHeaders({'Content-Type':'application/json'})};
    this.http.post("https://localhost:44329/api/contactus/",JSON.stringify(bankform.value),httpheader)
    .subscribe((data)=>
    {console.log(data);
    })
  }*/
  
}
