import { Component } from '@angular/core';

import * as HighCharts from 'highcharts'
import * as signalR from '@microsoft/signalr'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  connection: signalR.HubConnection;
  // data: any;
  dataSource2: any;

  isUpdated: boolean = false;
  chart: any;
  chartCallback: any;
  dataSource: Array<Object> = [];
  constructor() {
    this.connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:5001/saleshub").build();
    this.connection.start();

    this.connection.on("test", (message) => {
      console.log(message);
    });
    this.connection.on("receiveMessage", (message) => {

      console.log(message)
      // this.data = message;

      message.forEach((element: any) => {
        element.sales = element.sales.map((s: any) => s.price)
        this.dataSource.push({
          name: element.firstName + " " + element.lastName,
          type: 'line',
          data: element.sales
        })
      });
      // // this.result = employees[0].sales.map((s: any) => s.price)
      console.log(this.dataSource)
      // console.log(this.data)
      // console.log(this.dataSource)
      this.dataSource2 = this.dataSource;
      console.log(this.dataSource2)
      this.chartOptions.series = this.dataSource2;
      console.log(this.chart.series.length);

      this.handleUpdate()
      console.log(this.chart);

    })

    this.chartCallback = (ch: any) => {
      this.chart = ch;
    }
  }


  handleUpdate() {
    this.chartOptions.title = {
      text: 'updated'
    };
    for (let i = 0; i < this.chart.series.length; i++) {
      this.chart.series[i].remove();
    }
    for (let i = 0; i < this.chart.series.length; i++) {
      this.chart.series[i].remove();
    }
    for (let i = 0; i < this.dataSource2.length; i++) {
      this.chart.addSeries(this.dataSource2[i]);
    }
    this.dataSource2 = [];
    this.dataSource = [];
    console.log(this.chart.series);

    // this.isUpdated = true;
  }

  // chart: any;
  // updateFromInput = false;
  // chartCallback: any;

  Highcharts: typeof HighCharts = HighCharts;
  chartOptions: HighCharts.Options = {

    title: {
      text: "Header"
    },

    subtitle: {
      text: "Subheader"
    },
    yAxis: {
      title: {
        text: 'Axis Y'
      }
    },
    xAxis: {
      accessibility: {
        rangeDescription: "2019-2020"
      }
    },
    series: [
      {
        "name": "Ahmet Can Akdas",
        "type": "line",
        "data": [
          2950,
          3555
        ]
      },
      {
        "name": "John Doe",
        "type": "line",
        "data": [
          3300,
          2500,
          6100,
          4800
        ]
      },
      {
        "name": "James Alvarez",
        "type": "line",
        "data": [
          4950,
          5715
        ]
      }
    ],
    legend: {
      layout: "vertical",
      align: 'right',
      verticalAlign: 'middle'
    },
    plotOptions: {
      series: {
        label: {
          connectorAllowed: true
        },
        pointStart: 0
      }
    }
  }
}
