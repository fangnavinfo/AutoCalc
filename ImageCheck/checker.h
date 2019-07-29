#pragma once
#include <string>
#include <vector>

class ImageCheck
{
public:
	ImageCheck();
	~ImageCheck();

public:
	void Load(const std::string& path);
	int GetOver();
	int GetUnder();
	int GetTotal();

private:
	std::vector<float> vecPixl;
};

