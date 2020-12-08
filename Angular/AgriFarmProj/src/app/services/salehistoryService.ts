import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import { NgForm } from '@angular/forms';

@Injectable()
export class SaleHistoryService{
    url="https://localhost:44365/api/salehistory/";
    user=sessionStorage.getItem("user");
    constructor(private http:HttpClient){

    }

    getHistory(id:string){
        return this.http.get(this.url+"?id="+id+"&usertype="+this.user);
    }
}