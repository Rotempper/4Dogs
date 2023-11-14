import { CitysService } from 'src/app/services/citys.service';
import { Component, OnInit } from '@angular/core';
import { Citys } from 'src/app/model/citysModel';
import { Shops } from 'src/app/model/shopsModel';
import { ShopsService } from 'src/app/services/shops.service';
import { FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-shops',
  templateUrl: './shops.component.html',
  styleUrls: ['./shops.component.css']
})
export class ShopsComponent implements OnInit {

  public allShops: Array<Shops> = [];
  public allCitys: Array<Citys> = [];
  public token = this.AuthHttp.ShareToken;

  constructor(private ShopsHttp:ShopsService,
              private CitysHttp:CitysService,
              private AuthHttp: AuthenticationService){}

  ngOnInit(): void {
    this.GetShops();
    this.GetCitys();
  }

  // שליפת חנויות
  async GetShops():Promise<any>{
    this.allShops = await this.ShopsHttp.GetAllShops(this.token);
    console.log(this.allShops);
  };

  // שליפת ערים
  async GetCitys():Promise<any>{
    this.allCitys = await this.CitysHttp.GetAllCitys();
    console.log(this.allCitys);
  };

}
