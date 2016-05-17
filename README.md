1. 
每個人會有兩個remote repository，一個是group MetroRent repository，一個是personal MetroRent repository 
2. 
group MetroRent repository我已經建好。但要如何產生personal MetroRent repository？
Fork group MetroRent repository到你自己的帳號底下，也就是在group MetroRent repository點選左邊那個Fork，Fork完成會有一個自己的personal MetroRent repository URL，這個Fork repository URL是用來push code用的
3. 
Local端Visual Studio的project的Git Repository設定：
Fetch: https://YourBitbucketAccount@bitbucket.org/metrorent/metrorent.git
Push: https://YourBitbucketAccount@bitbucket.org/YourBitbucketAccount/metrorent.git
像我的就是
Fetch: https://ChenWeiTsai@bitbucket.org/metrorent/metrorent.git
Push: https://ChenWeiTsai@bitbucket.org/ChenWeiTsai/metrorent.git
4. 
目前repository已經有一個乾淨的MVC project code，請想辦法把code用git pull回你們自己的電腦上的visual studio project的資料夾，然後試著用visual studio把project打開後compile試看看
5. 
project內有兩個git branch: master和development，任何開發都在development branch下開發，也就commit請都commit到development branch，不要commit到master branch，master branch是用來deploy和做版本用的
6. 
任何commit請按照以下格式寫commit message：[Your Name] Something about the commit
。例如說現在專案的第一個commit是[Barry] Create a brand new ASP.NET MVC project with testing，這樣才清楚知道commit是誰發的，還有這個commit做了什麼事情

7. 
請不要直接push commit到group MetroRent repository，而是要先push commit到personal MetroRent repository，然後再到personal MetroRent repository發pull request到group MetroRent repository，最後由另外一個人去做code review，把剛剛的pull request真的merge回group MetroRent repository

8. 整體流程是
a) local端development branch開發，commit到自己的local development branch
b) push你local commit到personal MetroRent repository的development branch
c) 到你自己的personal MetroRent repository的網頁，點選create pull request，發pull request到group MetroRent repository
d) 找另外一組員幫你看code做code review，由他來grant你發的pull reqeust去把你的commit merge回group MetroRent repository

9. 
流程可能第一次看感覺有些複雜，如果需要幫忙我們可以約時間在學校，我可以幫忙Git等環境設定，和示範操作發pull request流程