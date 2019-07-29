#include "stdafx.h"

#include<numeric>
#include<opencv2\core\core.hpp>
#include<opencv2\highgui\highgui.hpp>
#include<opencv2\imgproc\imgproc.hpp>

#include "checker.h"

using std::accumulate;

//计算图像直方图
class Histogram1D
{
private:
	int histSize[1];
	float hranges[2];
	const float* ranges[1];
	int channels[1];
public:
	Histogram1D()
	{
		histSize[0] = 256;
		hranges[0] = 0.0;
		hranges[1] = 256.0;
		ranges[0] = hranges;
		channels[0] = 0;
	}
	cv::Mat getHistogram(const cv::Mat& image)
	{
		cv::Mat hist;
		cv::calcHist(&image,
			1,//仅为一个图像的直方图
			channels,//使用的通道
			cv::Mat(),//不使用掩码
			hist,//作为结果的直方图
			1,//这时一维的直方图
			histSize,//箱子数量
			ranges//像素值的范围
		);
		return hist;
	}
};

ImageCheck::ImageCheck()
{
}


ImageCheck::~ImageCheck()
{
}
  

void ImageCheck::Load(const std::string& path)
{
	vecPixl.clear();

	cv::Mat image = cv::imread(path);
	//cv::imshow("图片1", image);
	Histogram1D ch;
	cv::Mat histo = ch.getHistogram(image);
	for (int i = 0; i < 256; i++)
	{
		vecPixl.push_back(histo.at<float>(i));
	}
}

int ImageCheck::GetOver()
{
	return accumulate(vecPixl.begin() + 200, vecPixl.end(), 0);
}

int ImageCheck::GetUnder()
{
	return accumulate(vecPixl.begin(), vecPixl.begin() + 51, 0);
}

int ImageCheck::GetTotal()
{
	return accumulate(vecPixl.begin(), vecPixl.end(), 0);
}

