import  {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import {FarmerApproval} from '../models/farmerapproval.model';

@Injectable()
export class BidderMarketPlaceService{
    constructor(private http: HttpClient) { }

    GetCurrentSales(){
        //debugger;
        return this.http.get('https://localhost:44365/api/GetCurrentSales/')
    }
    // GetAllCrops(){
    //     return this.http.get('https://localhost:44365/api/AllCrops/')
    // }

    getCurrentCropById(){
        //debugger;
        //return this.http.get('https://localhost:44365/api/GetCropById/?id=4');
        return this.http.get('https://localhost:44365/api/GetCropById/?id='+sessionStorage.getItem('auctionid'));
    }

    PlaceBid(latestbid){
        return this.http.post('https://localhost:44365/api/PlaceBid/?id='+sessionStorage.getItem('auctionid')+"&bidderID="+sessionStorage.getItem('bid')+'&latestbid='+latestbid,'');
    }

}