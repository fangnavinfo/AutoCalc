#include "stdafx.h"

#include<numeric>
#include<opencv2\core\core.hpp>
#include<opencv2\highgui\highgui.hpp>
#include<opencv2\imgproc\imgproc.hpp>

#include "checker.h"

using std::accumulate;

//����ͼ��ֱ��ͼ
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
			1,//��Ϊһ��ͼ���ֱ��ͼ
			channels,//ʹ�õ�ͨ��
			cv::Mat(),//��ʹ������
			hist,//��Ϊ�����ֱ��ͼ
			1,//��ʱһά��ֱ��ͼ
			histSize,//��������
			ranges//����ֵ�ķ�Χ
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
	//cv::imshow("ͼƬ1", image);
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

