#include <stdio.h>
#include <stdlib.h>

#include <string>
#include <vector>
#include <algorithm>
#include <sstream>

#include "Util.h"

using namespace std;

vector<char> buf;
int size_buf = 1024*1024;

bool Manager(const string& name, const string& dir_out){
	FILE* f;
	if((f = fopen("./data/Manager.cs", "r")) == NULL) return false;
	int size = fread(&buf[0], 1, size_buf, f);
	string ss(buf.begin(), buf.begin()+size);
	fclose(f);
	string::size_type pos = 0;
	string target("#NAME#");
	while((pos = ss.find(target, pos)) != string::npos){
		ss.replace(pos, target.length(), name);
		pos += name.length();
	}
	string path = dir_out + name + string("Manager.cs");
	if((f = fopen(path.c_str(), "w")) == NULL) return false;
	fwrite(ss.c_str(), ss.length(), 1, f);
	fclose(f);
	return true;
}

bool State(const string& name, const vector<string>& stats, const string& dir_out){
	FILE* f;
	if((f = fopen("./data/State.cs", "r")) == NULL) return false;
	int size = fread(&buf[0], 1, size_buf, f);
	string ss(buf.begin(), buf.begin()+size);
	fclose(f);

	{
		string::size_type pos = 0;
		string target("#NAME#");
		while((pos = ss.find(target, pos)) != string::npos){
			ss.replace(pos, target.length(), name);
			pos += name.length();
		}
	}
	{
		string::size_type pos = 0;
		string target("#STATES#");
		stringstream ss_lines;
		for(auto ite = stats.begin();ite != stats.end();++ite){
			ss_lines << "        " << *ite << ",\n";
		}
		string lines = ss_lines.str();
		while((pos = ss.find(target, pos)) != string::npos){
			ss.replace(pos, target.length(), lines);
			pos += lines.length();
		}
	}
	{
		string::size_type pos = 0;
		string target("#ADD_STATE#");
		stringstream ss_lines;
		for(auto ite = stats.begin();ite != stats.end();++ite){
			ss_lines << "        " << "m_StateManager.AddState(State." << *ite << ", typeof(" << name << "State_" << *ite << "), owner);\n";
		}
		string lines = ss_lines.str();
		while((pos = ss.find(target, pos)) != string::npos){
			ss.replace(pos, target.length(), lines);
			pos += lines.length();
		}
	}


	string path = dir_out + name + string("State.cs");
	if((f = fopen(path.c_str(), "w")) == NULL) return false;
	fwrite(ss.c_str(), ss.length(), 1, f);
	fclose(f);
	return true;
}


bool State_(const string& name, const string& state, const string& dir_out){
	FILE* f;
	if((f = fopen("./data/State_.cs", "r")) == NULL) return false;
	int size = fread(&buf[0], 1, size_buf, f);
	string ss(buf.begin(), buf.begin()+size);
	fclose(f);

	{
		string::size_type pos = 0;
		string target("#NAME#");
		while((pos = ss.find(target, pos)) != string::npos){
			ss.replace(pos, target.length(), name);
			pos += name.length();
		}
	}
	{
		string::size_type pos = 0;
		string target("#STATE#");
		while((pos = ss.find(target, pos)) != string::npos){
			ss.replace(pos, target.length(), state);
			pos += state.length();
		}
	}
	stringstream path;
	path << dir_out << name << "State_" << state << ".cs";
	if((f = fopen(path.str().c_str(), "w")) == NULL) return false;
	fwrite(ss.c_str(), ss.length(), 1, f);
	fclose(f);
	return true;
}

int main(int argc, char** argv){
	if(argc < 3){
		printf("%s <name_state> <csv_stats>\n", *(argv+0));
		return 1;
	}
	string name(*(argv+1));
	vector<string> stats = Util::split(string(*(argv+2)), ',');

	buf.resize(size_buf);

	stringstream dir_out;
	dir_out	<< "./out/" << name << "State/";

	Manager(name, dir_out.str());
	State(name, stats, dir_out.str());
	for(auto ite = stats.begin();ite != stats.end();++ite){
		State_(name, *ite, dir_out.str());
	}
	return 0;
}
