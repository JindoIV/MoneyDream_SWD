"use client";
import React, { useEffect, useRef, useState } from "react";
import * as Highcharts from "highcharts";
import HighchartsReact from "highcharts-react-official";
import NoDataToDisplay from "highcharts/modules/no-data-to-display";
import { Spin } from "antd";
import { http } from "@/utils/config";

const sampleStages = [
  { stage: "Stage 1" },
  { stage: "Stage 2" },
  { stage: "Stage 3" },
];

const sampleData = [
  { stage: "Stage 1", count: 10 },
  { stage: "Stage 2", count: 20 },
  { stage: "Stage 3", count: 30 },
];

interface Stage {
  stage: string;
}

type StageOpportunity = {
  stage: string;
  count: number;
};

const BarChart = () => {
  const chartComponentRef = useRef<HighchartsReact.RefObject>(null);

  const [optionsChart, setOptionsChart] = useState<Highcharts.Options>({});
  const [stages, setStages] = useState<Stage[]>(sampleStages);
  const [dataChart, setDataChart] = useState<StageOpportunity[]>(sampleData);

  const convertDataChart = (data: StageOpportunity[]): any[] => {
    if (!data || data.length === 0) return [];

    return stages.map((stageItem: any, index) => {
      const current = data.find(
        (stage: Stage) => stage.stage === stageItem.stage
      );
      const HC = Highcharts.getOptions().colors as Highcharts.ColorString[];

      if (!current) {
        console.warn(`No data found for stage: ${stageItem.stage}`);
      }

      return {
        y: current ? current.count : 0,
        name: stageItem.stage,
        color: HC
          ? Highcharts.color(HC[index % HC.length])
              .brighten(-0.2)
              .get("rgb")
          : "#000000",
      };
    });
  };

  useEffect(() => {
    if (typeof window !== "undefined") {
      NoDataToDisplay(Highcharts);

      const NewOptions: Highcharts.Options = {
        chart: {
          type: "bar",
        },
        accessibility: {
          enabled: false,
        },
        title: {
          text: "",
        },
        lang: {
          noData: "NO_DATA",
        },
        noData: {
          style: {
            fontWeight: "bold",
            fontSize: "15px",
            color: "#303030",
          },
        },
        colors: Highcharts.getOptions().colors
          ? Highcharts.map(
              Highcharts.getOptions().colors as Highcharts.ColorString[],
              (color: Highcharts.ColorString) => {
                return {
                  radialGradient: {
                    cx: 0.5,
                    cy: 0.3,
                    r: 0.7,
                  },
                  stops: [
                    [0, color],
                    [1, Highcharts.color(color).brighten(-0.1).get("rgb")],
                  ],
                };
              }
            )
          : [],
        plotOptions: {
          series: {
            animation: {
              duration: 2000,
            },
            dataLabels: {
              enabled: true,
              filter: {
                operator: ">",
                property: "y",
                value: 0,
              },
            },
          } as any,
        },
        xAxis: {
          type: "category",
          lineWidth: 0,
          tickWidth: 0,
        },
        yAxis: {
          min: 0,
          type: "linear",
          title: {
            text: "",
          },
        },
        credits: {
          enabled: false,
        },
        series: [
          {
            name: "",
            dataLabels: {
              align: "right",
              text: '<span style="opacity: 0.8">{y} %</span>',
            },
            data: convertDataChart(dataChart),
          },
        ] as any,
        legend: {
          enabled: false,
        },
      };
      setOptionsChart(NewOptions);
    }
  }, []);

  const initChart = () => {
    NoDataToDisplay(Highcharts);
    return Highcharts;
  };

  return (
    <HighchartsReact
      highcharts={initChart()}
      options={optionsChart}
      ref={chartComponentRef}
    />
  );
};

export default BarChart;
