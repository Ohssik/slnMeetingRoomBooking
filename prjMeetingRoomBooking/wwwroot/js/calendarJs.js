document.addEventListener('DOMContentLoaded', () => {
    getCalendar((new Date()).getFullYear(), (new Date()).getMonth() + 1);
    let defaultM = (new Date()).getMonth() + 1;
    let defaultD = (new Date()).getDate();
    defaultM = defaultM < 10 ? `0${defaultM}` : defaultM;
    defaultD = defaultD < 10 ? `0${defaultD}` : defaultD;
    let markedDate = document.getElementById(`${(new Date()).getFullYear()}-${defaultM}-${defaultD}`);
    markedDate.classList.add('bgcMarked');

    let monthList=document.getElementById('monthList');
    for(let i=1;i<=12;i++){
        let M=document.createElement('option');
        M.value=i;
        M.innerHTML=i;                                
        monthList.appendChild(M);
    }

    let thisYear = (new Date()).getFullYear();
    let yearList=document.getElementById('yearList');
    for (let i = thisYear - 5; i <= thisYear+5;i++){
        let y=document.createElement('option');
        y.value=i;
        y.innerHTML=i;                
        yearList.appendChild(y);
    }

    
    let sls=document.getElementsByName('sl');
    let selectedDate=document.getElementById('dateList');
    let showDate = document.getElementById('showDate');
    let y = (new Date().getFullYear()),
        m = (new Date().getMonth() + 1),
        dd, getLastDate, getLastWeekDay, getFirstWeekDay;
    
    for(let sl of sls){
        
        sl.addEventListener('change',()=>{
            
            showDate.innerHTML="日期選擇中";
            if(sl.id=='yearList'){
                y=sl.value;
                
            }else if(sl.id=='monthList'){
                m=sl.value;                
            }

            if(parseInt(m)>0 && parseInt(y)>0)
            {
                getCalendar(y, m);                
            }
        });
    }

    //document.getElementById('dateList').addEventListener('change',()=>{
    //    let dates=document.querySelectorAll('.calendarItem');
    //    for(let date of dates){            
    //        date.classList.remove('bgcMarked');
    //    }
    //    dd=selectedDate.value;
    //    //showDate.innerHTML=`您選擇的日期是 ${y} 年 ${m} 月 ${dd} 日`;
    //    let markedDate=document.getElementById(`${y}-${m}-${dd}`);
    //    markedDate.classList.add('bgcMarked');
    //});
    
    function getCalendar(y,m) {
        let getLastDate = (new Date(y, m, 0)).getDate();
        let getLastWeekDay = (new Date(y, m, 0)).getDay();
        let getFirstWeekDay = (new Date(y, m - 1, 1).getDay());

        //let dateList = document.getElementById('dateList');
        //for (let i = 1; i <= getLastDate; i++) {
        //    let d = document.createElement('option');
        //    d.value = i;
        //    d.innerHTML = i;

        //    dateList.appendChild(d);
        //}

        let oldDateItems = document.querySelectorAll('#showCalendar div');
        for (let item of oldDateItems) {
            item.remove();
        }
        let showCalendar = document.getElementById('showCalendar');
        let wds = ['日', '一', '二', '三', '四', '五', '六'];
        for (let i = 0; i < 7; i++) {
            let weekDay = document.createElement('div');
            weekDay.className = 'calendarItem bgc';
            weekDay.innerHTML = wds[i];
            showCalendar.appendChild(weekDay);
        }

        m = m < 10 ? `0${m}` : m;
        let date;
        for (let i = 0; i < 42; i++) {

            let dateItem = document.createElement('div');
            dateItem.className = 'calendarItem';
            
            if (i >= getFirstWeekDay && i < getLastDate + getFirstWeekDay) {
                dateItem.innerHTML = i - (getFirstWeekDay - 1);                
                date = i - (getFirstWeekDay - 1);
                date = date < 10 ? `0${date}` : m;
                dateItem.id = `${y}-${m}-${date}`;
            }
            showCalendar.appendChild(dateItem);
        }
    }
    
});