import { ReplaceableComponentsService } from '@abp/ng.core';
import { Component, OnInit} from '@angular/core';
import { LoginComponent } from './login/login.component';
import { eAccountComponents } from '@abp/ng.account';

@Component({
  selector: 'app-root',
  template: `
    <abp-loader-bar></abp-loader-bar>
    <abp-dynamic-layout></abp-dynamic-layout>
  `,
})
export class AppComponent implements OnInit{
  constructor(private replaceableComponentsService: ReplaceableComponentsService) {}

  ngOnInit() {
    console.log('ngOnInit called');
    // this.replaceableComponentsService.add({
    //   key: eAccountComponents.Login,
    //   component: LoginComponent,
    // });
  }
}