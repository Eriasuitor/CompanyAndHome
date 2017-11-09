#include<iostream>
#include<algorithm>
#include<cstdio>
#include<set>
#include<string.h>
#include<list>
using namespace std;
#define MAX_DATA_NUM 5000
struct DATA
{
    int w;
    int h;
    int index;
    long long square;
};
DATA data[MAX_DATA_NUM + 50];
list<int> Record[MAX_DATA_NUM + 50];
int ansMin[MAX_DATA_NUM];
int cmp(DATA a, DATA b)
{
    if (a.square == b.square)
        return a.index < b.index;
    else
        return a.square < b.square;
}
int pdChange(int indexnow, int indexans)
{
    int a[MAX_DATA_NUM + 50];
    int b[MAX_DATA_NUM + 50];
    list<int>::iterator it;
    int ACnt = 0;
    int BCnt = 0;
    for (it = Record[indexnow].begin(); it != Record[indexnow].end(); it++)
        a[ACnt++] = *it;
    for (it = Record[indexans].begin(); it != Record[indexans].end(); it++)
        b[BCnt++] = *it;
    sort(a, a + ACnt);
    sort(b, b + BCnt);
    for (int i = 0; i < ACnt; i++)
    {
        if (a[i] < b[i])
            return 1;
        else if (a[i] > b[i])
            return 0;
    }
    return 0;
}
int cldata(int n, int w, int h)
{
    int nCnt = 0;
    DATA data2[MAX_DATA_NUM + 50];
    for (int i = 0; i < n; i++)
    {
        if (data[i].w < w || data[i].h < h)
            continue;
        if (i && data[i].w == data[i - 1].w && data[i].h == data[i - 1].h)
            continue;
        data2[nCnt++] = data[i];
    }
    for (int i = 0; i < nCnt; i++)
    {
        data[i] = data2[i];
    }
    return nCnt;
}

int main()
{
    int n, w, h;
    while (scanf("%d%d%d", &n, &w, &h) != EOF)
    {
        memset(ansMin, -1, sizeof(ansMin));
        int ansLen = 0;
        int ansIndex = -1;
        for (int i = 0; i < n; i++)
        {
            int ww, hh;
            scanf("%d%d", &ww, &hh);
            data[i].w = ww;
            data[i].h = hh;
            data[i].index = i + 1;
            data[i].square = (long long)ww* hh;
    }
    sort(data, data + n, cmp);
    n = cldata(n, w, h);

    for (int i = 0; i < n; i++)
    {
        Record[i].clear();
        int nowAnsIndex = -1;
        int nowAnsLen = 0;
        for (int j = n - 1; j >= 0; j--)
        {
            if (data[i].w > data[j].w && data[i].h > data[j].h && (Record[j].size() > nowAnsLen || (Record[j].size() == nowAnsLen && nowAnsLen != 0 && ansMin[j] < ansMin[nowAnsIndex])))
            {
                nowAnsIndex = j;
                nowAnsLen = Record[j].size();
            }
        }
        if (nowAnsIndex != -1)
        {
            list < int > &nowAns = Record[nowAnsIndex];
            for (list<int>::iterator it = nowAns.begin(); it != nowAns.end(); it++)
            {
                Record[i].push_back(*it);
            }
            Record[i].push_back(data[i].index);
            ansMin[i] = min(ansMin[nowAnsIndex], data[i].index);
        }
        else if (data[i].w > w && data[i].h > h)
        {
            Record[i].push_back(data[i].index);
            ansMin[i] = data[i].index;
        }
    }
    for (int i = 0; i < n; i++)
    {
        list < int > &nowlist = Record[i];
        if (nowlist.size() > ansLen || (nowlist.size() == ansLen && ansLen != 0 && pdChange(i, ansIndex)))
        {
            ansLen = nowlist.size();
            ansIndex = i;
        }
    }
    printf("%d\n", ansLen);
    if (ansIndex > -1)
    {
        for (list<int>::iterator it = Record[ansIndex].begin(); it != Record[ansIndex].end(); it++)
        {
            if (it != Record[ansIndex].begin())
                printf(" ");
            printf("%d", *it);
        }
        printf("\n");
    }
}
    return 0;
}