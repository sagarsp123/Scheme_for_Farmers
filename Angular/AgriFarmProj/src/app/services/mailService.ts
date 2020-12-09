import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import { NgForm } from '@angular/forms';

@Injectable()
export class MailService{
    url="https://localhost:44365/"; 

    constructor(private http:HttpClient){

    }

    sendMail(mail:NgForm){
        console.log(mail.value);
        const httpheader={headers:new HttpHeaders({'Content-Type':'application/json'})};
        //debugger;
        return this.http.post(this.url+"api/mail/",JSON.stringify(mail.value),httpheader);
    }

    changestatus(mail:string){
        const httpheader={headers:new HttpHeaders({'Content-Type':'application/json'})};
        //debugger;
        //return this.http.put(this.url+"?mail="+mail+"&id="+sessionStorage.getItem("aid"),httpheader);
        return this.http.post("https://localhost:44365/api/EmailSent/?mail="+mail+"&id="+sessionStorage.getItem("aid"),"");
    }
    ApprovalMail(usermail:string){
        return this.http.post(this.url+"approvalmail?to="+usermail,"");
    }

    ClaimApproval(policy:number){
        return this.http.post(this.url+"claimapproval?policyno="+policy,"");
    }

}