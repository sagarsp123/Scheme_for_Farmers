import  {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import {BidderApproval} from '../models/bidderapproval.model';

@Injectable()
export class AdminApproveBidderService{
    url="https://localhost:44365/api/";
    constructor(private http: HttpClient) { }
    GetUnapprovedBidders(){
        //debugger;
        return this.http.get(this.url+"GetUnapprovedBidders/");
    }

    updateBidder(eg){
        const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
        eg.BidderApproved=true;
        eg.ApprovalAdminId=sessionStorage.getItem("aid");
        console.log(JSON.stringify(eg));
        //put
        return this.http.post(this.url+"ApproveBidderAdmin/?id="+ eg.Bidderid+"&adminid="+eg.ApprovalAdminId,"");        
      }
}