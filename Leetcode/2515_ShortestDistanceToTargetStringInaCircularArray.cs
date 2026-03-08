// https://leetcode.com/problems/shortest-distance-to-target-string-in-a-circular-array/description/

using System;
using System.Data.Odbc;

string[] words = ["lwgdugypkmsvxwhwbrccrbtemvudwhctnaaonednsbodptendi","lumylagwxpmmivpujfawgvdbtxpluwekdpeakrqelpvrflnsnr","lngqwiijizfzzhlvvszaownnachqunlktsnhgsjeluvcpmavuj","nabbqiyarxmzleesxrfaynypfxonyhvwhebfjsxyinepggxnns","oiqghjtvrhpgvsjlzmrwbwpmomqqliqytdzawhkwematskflgf","dtiwjpdgcsmhaiwxrgligxlibfmvclobjjhljrqlvpuiufskix","svqgvooghuqszkrmcrayqclotygdqnxfetdrfrfvbypgiizzdk","qzrmfzdiodkdbhwifsinmamljlztwqtaoivufkcyeydsvpmdzw","ihaekjyxvnmhvtanysybyqvrtmffpkgmnxisgmmhkhbtonlwgo","ucrvwdlifpckbimcvevgsniepuewjqpakwnxksumgvrnmhmtuk","lngqwiijizfzzhlvvszaownnachqunlktsnhgsjeluvcpmavuj","lngqwiijizfzzhlvvszaownnachqunlktsnhgsjeluvcpmavuj","vdtvcclyyraevotgikgojlbefpfmlzypychxehnglgettackoz","qxspwpzxfxyxalrjuliwtbyllamkifbknnhzyfeabavwvvdkzk","vdtvcclyyraevotgikgojlbefpfmlzypychxehnglgettackoz","ucrvwdlifpckbimcvevgsniepuewjqpakwnxksumgvrnmhmtuk","dtiwjpdgcsmhaiwxrgligxlibfmvclobjjhljrqlvpuiufskix","vtbfahotrkxwcfwzlijfoqdkrvdmavpllbcfvibrqeuntsmrcs","mfhqksxfeeltpiupaijavavbpphjxyuzqlqehirxnmviiaqnfv","oowbnwbktlmsawtbilyvksqkbuohhjxqbepxgklkrwpjzcvipe","mpnnvwuqbczvmrwhzvsmtuumwjczqehuumjvfbpgoxlurjbckq","byaschxhjcgnnodazzpisqriyszffaqqiwljbuigdvzzobrlmc","dmctcimgeztojrvqwyygmnzfwtsrmmbgednmytsludnrpjjjvk","qxspwpzxfxyxalrjuliwtbyllamkifbknnhzyfeabavwvvdkzk","cawzflwjjopbemxqazpsrsrlxljwqlvzpvjbjarqeqgythrsou","ydqjqvalipkkrcbdprgjjangclswdjpaajiwhxeupidxirlith","lwgdugypkmsvxwhwbrccrbtemvudwhctnaaonednsbodptendi","ejtkmbyqtwrlhwynnqggpjaaaydjqnczhtyphfgugpbardzlvj","cawzflwjjopbemxqazpsrsrlxljwqlvzpvjbjarqeqgythrsou","oowbnwbktlmsawtbilyvksqkbuohhjxqbepxgklkrwpjzcvipe","khhwlijglujhgimvfvuwgggigppichzscdtsiklalgqeqsencq","khhwlijglujhgimvfvuwgggigppichzscdtsiklalgqeqsencq","lngqwiijizfzzhlvvszaownnachqunlktsnhgsjeluvcpmavuj","frdsoraagsllmgtatzybegxotrtgsuwfzpzxkpegggvpodnhrr","ynuartuisymsqxxdqwndonpqhczqpuekwbayfidfisjpriqogx","shmpixycafoqskoegqfvsrvboiegqwlbrkiuoeetncdxqlqsov","oiqghjtvrhpgvsjlzmrwbwpmomqqliqytdzawhkwematskflgf","xjtatoefvpwwgsvmepltadmzngxtnahqypfxgjppbqsmqnjvxm","vtbfahotrkxwcfwzlijfoqdkrvdmavpllbcfvibrqeuntsmrcs","dmctcimgeztojrvqwyygmnzfwtsrmmbgednmytsludnrpjjjvk","dsohnrdxdqrbwdjhqpphwvlzfyizqyoedckthdlhmkxjxewubc","qriythowwswwwucgwmezpqqneatemdxfqzpeytlozzojguywby","lyhmqyjnztwzqotqkpmvpueyzjfroousgkkerqvmwjnjtmlkuf","qzrmfzdiodkdbhwifsinmamljlztwqtaoivufkcyeydsvpmdzw","qzrmfzdiodkdbhwifsinmamljlztwqtaoivufkcyeydsvpmdzw","gxrtwoayoosijaddrlbvxqsvbbvaziqemcpxgljlnexvjnnakk","mjftbskulmuztejkopyftjsdeoyuvhvqragbkqlfhgqqkafvau","mjftbskulmuztejkopyftjsdeoyuvhvqragbkqlfhgqqkafvau","qzrmfzdiodkdbhwifsinmamljlztwqtaoivufkcyeydsvpmdzw","qxspwpzxfxyxalrjuliwtbyllamkifbknnhzyfeabavwvvdkzk"];
var result = new Solution().ClosestTarget(words, "ydqjqvalipkkrcbdprgjjangclswdjpaajiwhxeupidxirlith", 0);
Console.WriteLine(result);

public class Solution {
    public int ClosestTarget(string[] words, string target, int startIndex) {
        int left = startIndex; int stepsLeft = 0;
        int right = startIndex; int stepsRight = 0;

        while (true)
        {
            if (words[left] == target)
            {
                return stepsLeft;
            }
            if (words[right] == target)
            {
                return stepsRight;
            }

            left--; stepsLeft++;
            right++; stepsRight++;

            // we already moved, so just put it in a valid position
            if (left < 0)
            {
                left = words.Length - 1;
            }
            if (right >= words.Length)
            {
                right = 0;
            }

            if (stepsLeft + stepsRight > words.Length)
            { 
                // cannot find
                return -1;
            }
        }
    }
}