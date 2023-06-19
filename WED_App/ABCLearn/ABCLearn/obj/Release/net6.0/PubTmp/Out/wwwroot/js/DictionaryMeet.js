let word = "";
let meaning = [];
let phonetic = [];
document.getElementById("word").addEventListener('change', (e) => {
    if (e != null) word = e.target.value;
    //console.log("hehe");

    axios.get(
        `https://api.dictionaryapi.dev/api/v2/entries/en/${word}`
    ).then(
        (res) => {
            tempPhonetic = "<h5>Phonology</h5>";
            for (let index = 1; index < res.data[0].phonetics.length; index++) {
                tempPhonetic += `<i class="phonic">${res.data[0].phonetics[index].text}</br></i>`;
            }

            function PrintDefi(data) {
                //console.log(data.definitions[0])
                tempDefinition = "";
                for (let index = 0; index < data.definitions.length; index++) {
                    tempDefinition += `<p class="definition">${data.definitions[index].definition}</p>`;
                }
                return tempDefinition;
            }

            tempMeaning = "";
            for (let index = 0; index < res.data[0].meanings.length; index++) {
                tempMeaning += `<div class="wrap-meaning">
																	<h5 class="part-of-speech">${res.data[0].meanings[index].partOfSpeech}</h5>
																	 <p class="definition">
																		 ${PrintDefi(res.data[0].meanings[index])}
																	 </p>
																</div>`;
            }
            //console.log(res.data[0]);
            //console.log(res.data[0].phonetics[0].text);
            document.getElementById("title").innerText = res.data[0].word;
            document.getElementById("phonetic").innerHTML = tempPhonetic + "<hr/>";
            document.getElementById("meaning").innerHTML = tempMeaning + "<hr/>";
            document.querySelector(".definition").innerHTML = tempDefinition + "<hr/>";
            //document.querySelector("#contentDictionary").style.height = "46%";
        }
    ).catch(() => {
        document.getElementById("title").innerText = `NOT FOUND THE WORD ${word}`;
        document.getElementById("phonetic").innerHTML = "";
        document.getElementById("meaning").innerHTML = "";
        document.querySelector(".definition").innerHTML = "";
        //document.querySelector("#contentDictionary").style.height = "90%";
    })
});