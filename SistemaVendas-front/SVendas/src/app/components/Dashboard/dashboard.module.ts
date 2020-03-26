import { NgModule } from '@angular/core';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { SharedModule } from 'src/app/shared/modules/material/shared.module';
import { CommonModule } from '@angular/common';
import { GoogleChartsModule } from 'angular-google-charts';
import * as FusionTheme from 'fusioncharts/themes/fusioncharts.theme.fusion';
import * as FusionCharts from 'fusioncharts';
import * as Charts from 'fusioncharts/fusioncharts.charts';
import { FusionChartsModule } from 'angular-fusioncharts';

FusionChartsModule.fcRoot(FusionCharts, Charts, FusionTheme);

@NgModule({
  declarations: [
     DashboardComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    DashboardRoutingModule,
    GoogleChartsModule.forRoot(),
    FusionChartsModule,
  ]
})
export class DashboardModule { }
