import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Citys } from 'src/app/model/citysModel';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { CitysService } from 'src/app/services/citys.service';
import { ShopsService } from 'src/app/services/shops.service';

@Component({
  selector: 'app-addShop-Admin',
  templateUrl: './addShop-Admin.component.html',
  styleUrls: ['./addShop-Admin.component.css']
})
export class AddComponent implements OnInit {

  public allCitys: Array<Citys> = [];
  public token = this.AuthHttp.ShareToken;

  // חנות
  public AddShopForm= new FormGroup({
    nameShop: new FormControl('',[Validators.required]),
    aaddress: new FormControl('',[Validators.required]),
    cityId: new FormControl('',[Validators.required]),
    phone: new FormControl('',[Validators.required,Validators.pattern("[0-9 ]{10}")])
  });

  constructor(private CitysHttp: CitysService,private ShopHttp:ShopsService,
              private router: Router,private AuthHttp: AuthenticationService){}

  ngOnInit(): void {
    this.GetCitys();
  }

  // הוספת חנות
  async AddShops(){
    await this.ShopHttp.AddShop(this.AddShopForm.value,this.token);
    this.router.navigate(['/Admin']).then(() => {
      window.location.reload();
    });
  }

  // שליפת ערים
  async GetCitys():Promise<any>{
    this.allCitys = await this.CitysHttp.GetAllCitys();
  };

  // כפתור הקודם
  async onBack(){
    this.router.navigate([]).then(() => {
      window.location.reload();
    });
  }
}


