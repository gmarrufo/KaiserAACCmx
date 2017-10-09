//***************************************
//*  Copyright 2003-2004 ScanSoft, Inc.
//*  All Rights Reserved
//***************************************

//
// confirmations.es
//
// Default confirmation functions
//
// Andrew Simpson 24 August 2002
//

var monthTTS = new Array("empty", "january", "february", "march", "april", "may", "june",
                         "july", "august", "september", "october", "november", "december");

var ordinalTTS = new Array("empty", "first", "second", "third", "fourth", "fifth", "sixth",
                           "seventh", "eighth", "ninth", "tenth", "eleventh", "twelfth",
                           "thirteenth", "fourteenth", "fifteeth", "sixteenth", "seventeenth",
                           "eighteenth", "nineteenth", "twentieth", "twenty first", "twenty second",
                           "twenty third", "twenty fourth", "twenty fifth", "twenty sixth",
                           "twenty seventh", "twenty eighth", "twenty ninth", "thirtieth", "thirty first");

/*******************************************
* confirmCommand
*******************************************/
function confirmCommand(args)
{
  // Confirmation arguments
  var directory = args.baselinepromptsdirectory + "command/";
  var confValue = args.dm_confirm_string;

  // Create prompts object
  var prompts = new Object();
  prompts.audio = new Array();
  prompts.tts = new Array();

  // Add prompts
  prompts.audio.push(directory + confValue.replace(/\s/g,"%20") + ".ulaw");
  prompts.tts.push(confValue);

  return prompts;
}

/*******************************************
* confirmAlphanum
*******************************************/
function confirmAlphanum(args)
{
  // Loop Counters
  var c = 0;

  // Confirmation arguments
  var directory = args.baselinepromptsdirectory + "chars/";
  var confValue = args.value;

  // Create prompts object
  var prompts = new Object();
  prompts.audio = new Array();
  prompts.tts = new Array();

  //Getting the MEANING key if it is an object
  if (typeof confValue == 'object')
  {
    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
      confValue = confValue.MEANING;
    else
      confValue = confValue.dm_root;
  }

  // Break into an array of characters
  var characters = confValue.match(/\w/g);

  // Add prompts to the array
  for (c = 0; c < characters.length; c++)
  {
    prompts.audio.push(directory + characters[c].charCodeAt(0) + ".ulaw");
    prompts.tts.push(characters[c]);
  }

  return prompts;
}

/*******************************************
* confirmCCExpDate
*******************************************/
function confirmCCExpDate(args)
{
  // Loop Counters
  var i = 0;

  // Confirmation arguments
  var directory = args.baselinepromptsdirectory;
  var confValue = args.value;

  var prompts = new Object();

  // Initialize the prompts array
  prompts.audio = new Array();
  prompts.tts   = new Array();

  // Getting the MEANING key if it is an object
  if (typeof confValue == 'object')
  {
    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
      confValue = confValue.MEANING;
    else
      confValue = confValue.dm_root;
  }

  var century = confValue.substr( 0 , 2 );
  var month   = confValue.substr( 4 , 2 );
  var year2   = confValue.substr( 2 , 2 );
  var year4   = confValue.substr( 0 , 4 );

  // End-of prompts
  prompts.audio.push(directory + "ccexpdate/end_of.ulaw");
  prompts.tts.push("end of");

  // Month
  prompts.audio.push(directory + "month/" + month + ".ulaw");
  prompts.tts.push(monthTTS[Math.round(month)]);

  // Year
  if (century < 20)
  {
    prompts.audio.push(directory+"cardinal/" + century + ".ulaw");
    prompts.tts.push(century);
    prompts.audio.push(directory+"cardinal/" + year2 + ".ulaw");
    prompts.tts.push(year2);
  }
  else
  {
    var yearPrompts = new Array();
    yearPrompts = naturalNumber( year4  );

    for (i = 0; i < yearPrompts.audio.length; i++)
    {
      prompts.audio.push(directory + yearPrompts.audio[i]);
      prompts.tts.push(yearPrompts.tts[i]);
    }
  }

  return prompts;
}

/*******************************************
* confirmCreditCard
*******************************************/
function confirmCreditCard(args)
{
  // Loop Counters
  var i = 0;
  var j = 0;

  // Confirmation arguments
  var directory = args.baselinepromptsdirectory;
  var confValue = args.value;
  var cardtype = args.value.CARDTYPE;

  // Getting the MEANING key if it is an object
  if (typeof confValue == 'object')
  {
    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
      confValue = confValue.MEANING;
    else
      confValue = confValue.dm_root;
  }

  var prompts = new Object();
  var numbers = new Array();

  //Initialize the prompts array
  prompts.audio = new Array();
  prompts.tts   = new Array();

  if (cardtype!=null && cardtype.length != 0 && cardtype != "private")
  {
    prompts.audio.push(directory + "creditcard/" + cardtype + ".ulaw");
    prompts.tts.push(cardtype);

    prompts.audio.push(directory + "silence500ms.ulaw");
    prompts.tts.push("missing pause prompt");
  }

  if (confValue != null && confValue != "")
  {
    if (cardtype == "amex" )
    {
      // grouped as 4/6/5 (tot 15)
      numbers.push(confValue.substr(0,4));
      numbers.push(confValue.substr(4,6));
      numbers.push(confValue.substr(10,5));
    }
    else if (cardtype == "mastercard" || cardtype == "visa" || cardtype == "discover")
    {
      if (confValue.length == 16)
      {
        // mastercard, visa and discover, grouped as 4/4/4/4 (tot 16)
        numbers.push(confValue.substr(0,4));
        numbers.push(confValue.substr(4,4));
        numbers.push(confValue.substr(8,4));
        numbers.push(confValue.substr(12,4));
      }
      else
      {
        // mastercard and visa, grouped as 4/3/3/3 (tot 13)
        numbers.push(confValue.substr(0,4));
        numbers.push(confValue.substr(4,3));
        numbers.push(confValue.substr(7,3));
        numbers.push(confValue.substr(10,3));
      }
    }
    else if (cardtype == "dinersclub")
    {
      // grouped as 4/6/4 (tot 14)
      numbers.push(confValue.substr(0,4));
      numbers.push(confValue.substr(4,6));
      numbers.push(confValue.substr(10,4));
    }

    /* PlayAudioForCreditcard doen't know any other credit card number format */
    if (cardtype != "amex" && cardtype != "dinersclub" && cardtype != "discover" &&
        cardtype != "visa" && cardtype != "mastercard")
    {
      numbers.push(confValue);
    }

    /* Playing credit card numbers with silence prompts in between grouped numbers */
    for (i = 0; i < numbers.length; i++)
    {
      var toalphanum = new Object();
      toalphanum.value = numbers[i];
      toalphanum.baselinepromptsdirectory = directory;
      var spellprompt = confirmAlphanum(toalphanum);

      for (j = 0; j < spellprompt.audio.length; j++)
      {
        prompts.audio.push(spellprompt.audio[j]);
        prompts.tts.push(spellprompt.tts[j]);
      }

      if (i < (numbers.length - 1))
      {
        prompts.audio.push(directory + "silence500ms.ulaw");
        prompts.tts.push("missing pause prompt");
      }
    }
  }

  return prompts;
}

/*******************************************
* confirmCurrency
*******************************************/
function confirmCurrency(args)
{
  // Loop Counters
  var j = 0;
  var k = 0;;

  // Confirmation arguments
  var directory = args.baselinepromptsdirectory;
  var confValue = args.value;

  var prompts = new Object();

  // Initialize the prompts array
  prompts.audio = new Array();
  prompts.tts   = new Array();

  // Getting the MEANING key if it is an object
  if (typeof confValue == 'object')
  {
    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
      confValue = confValue.MEANING;
    else
      confValue = confValue.dm_root;
  }

  var dollars;
  var cents;

  var currArray = confValue.split('.')

  if (currArray.length == 2)
  {
    dollars = currArray[0];
    cents   = currArray[1];
  }

  // Handle invalid amounts by simply using it as the filename
  if (dollars == null || cents == null)
  {
    prompts.audio.push(directory + confValue);
    prompts.tts.push(confValue);
    return prompts;
  }

  var spellPromptDollars = naturalNumber(dollars);
  var spellPromptCents = naturalNumber(cents);

  // Check if both dollars and cents are zero
  // Notice that we can compare to '0' since the first tts prompt is the lowest
  // cardinal number which is not 0 (as returned by naturalNumber)
  if (spellPromptDollars.tts[0] == '0' && spellPromptCents.tts[0] == '0')
  {
    // Play "zero"
    prompts.audio.push(directory + spellPromptDollars.tts[0]);
    prompts.tts.push("0");

    // Play "dollars"
    prompts.audio.push(directory + "currency/dollars.ulaw");
    prompts.tts.push("dollars");

    // Play "and"
    prompts.audio.push(directory + "currency/and.ulaw");
    prompts.tts.push("and");

    // Play "zero"
    prompts.audio.push(directory + spellPromptCents.tts[0]);
    prompts.tts.push("0");

    // Play "cents"
    prompts.audio.push(directory + "currency/cents.ulaw");
    prompts.tts.push("cents");
  }
  else
  {
    // Do not play "zero dollars"
    if (spellPromptDollars.tts[0] != '0')
    {
      // Play all the prompts necessary to represent to ammount of dollars said
      for (j = 0; j < spellPromptDollars.audio.length; j++)
      {
        prompts.audio.push(directory + spellPromptDollars.audio[j]);
        prompts.tts.push(spellPromptDollars.tts[j]);
      }

      // Determine whether to play "dollars" or "dollar"
      if (parseInt(dollars, 10) > 1)
      {
        prompts.audio.push(directory + "currency/dollars.ulaw");
        prompts.tts.push("dollars");
      }
      else
      {
        prompts.audio.push(directory + "currency/dollar.ulaw");
        prompts.tts.push("dollar");
      }
    }

    // Do not play "zero cents"
    if (spellPromptCents.tts[0] != '0')
    {
      // Play "and" if there is a dollar amount specified
      if (spellPromptDollars.tts[0] != '0')
      {
        prompts.audio.push(directory + "currency/and.ulaw");
        prompts.tts.push("and");
      }

      // Play all the prompts necessary to represent to ammount of cents said
      for (k = 0; k < spellPromptCents.audio.length; k++)
      {
        prompts.audio.push(directory + spellPromptCents.audio[k]);
        prompts.tts.push(spellPromptCents.tts[k]);
      }

      // Determine whether to play "cents" or "cent"
      if (parseInt(cents, 10) > 1)
      {
        prompts.audio.push(directory + "currency/cents.ulaw");
        prompts.tts.push("cents");
      }
      else
      {
        prompts.audio.push(directory + "currency/cent.ulaw");
        prompts.tts.push("cent");
      }
    }
  }

  return prompts;
}

/*******************************************
* confirmCustomContext
*******************************************/
function confirmCustomContext(args)
{
  // Confirmation arguments
  var directory = args.baselinepromptsdirectory;
  var confValue = "";

  if (typeof(args.dm_confirm_string) != 'undefined' && args.dm_confirm_string != null)
    confValue = args.dm_confirm_string;
  else if (typeof(args.dm_root) != 'undefined' && args.dm_root != null)
    confValue = args.dm_root;
  else if (typeof(args.MEANING) != 'undefined' && args.MEANING != null)
    confValue = args.MEANING;

  // Result object
  var prompts = new Object();

  // Initialize the prompts array
  prompts.audio = new Array();
  prompts.tts = new Array();

  // Set confirmation prompt and tts values in the return object's arrays
  prompts.audio.push(directory + confValue.replace(/\s/g,"%20") + ".ulaw");
  prompts.tts.push(confValue);

  return prompts;
}

/*******************************************
* confirmDate
*******************************************/
function confirmDate(args)
{
  // Loop Counters
  var i = 0;

  // Prompts Directory
  var directory = args.baselinepromptsdirectory;
  // Choose meaning for a builtin
  var confValue = args.value;

  // Result object
  var prompts = new Object();

  // Initialize the prompts array
  prompts.audio = new Array();
  prompts.tts = new Array();

  //Getting the MEANING key if it is an object
  if (typeof confValue == 'object')
  {
    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
      confValue = confValue.MEANING;
    else
      confValue = confValue.dm_root;
  }

  // Parse the value string YYYYMMDD
  var year4 = confValue.substr(0,4);
  var century = confValue.substr(0,2);
  var year2 = confValue.substr(2,2);
  var month = confValue.substr(4,2);
  var day = confValue.substr(6,2);

  // Month
  prompts.audio.push(directory + "month/" + month + ".ulaw");
  prompts.tts.push(monthTTS[Math.round(month)]);
  // Day
  prompts.audio.push(directory + "ordinal/" + day + ".ulaw");
  prompts.tts.push(ordinalTTS[Math.round(day)]);
  // Year
  if (century < 20)
  {
    prompts.audio.push(directory + "cardinal/" + century + ".ulaw");
    prompts.tts.push(century);
    prompts.audio.push(directory + "cardinal/" + year2 + ".ulaw");
    prompts.tts.push(year2);
  }
  else
  {
    var yearPrompts = naturalNumber(year4);

    // Don't say 200-zero.  So if the last number is a zero, then
    // don't say it.
    if (yearPrompts.tts[yearPrompts.tts.length - 1] == '0')
    {
      for (i = 0; i < yearPrompts.audio.length - 1; i++)
      {
        prompts.audio.push(directory + yearPrompts.audio[i]);
        prompts.tts.push(yearPrompts.tts[i]);
      }
    }
    else
    {
      for (i = 0; i < yearPrompts.audio.length; i++)
      {
        prompts.audio.push(directory + yearPrompts.audio[i]);
        prompts.tts.push(yearPrompts.tts[i]);
      }
    }
  }

  return prompts;
}

/*******************************************
* confirmDigits
*******************************************/
function confirmDigits(args)
{
  // Loop Counters
  var d = 0;

  var directory = args.baselinepromptsdirectory+"cardinal/";
  var confValue = args.value;

  // Result object
  var prompts = new Object();
  prompts.audio = new Array();
  prompts.tts = new Array();

  //Getting the MEANING key if it is an object
  if (typeof confValue == 'object')
  {
    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
      confValue = confValue.MEANING;
    else
      confValue = confValue.dm_root;
  }

  // Break into an array of digits
  var digits = confValue.match(/\d/g);

  for (d = 0; d < digits.length; d++)
  {
   prompts.audio.push(directory + digits[d] + ".ulaw");
   prompts.tts.push(digits[d]);
  }

  return prompts;
}

/*******************************************
* confirmNaturalNumbers
*******************************************/
function confirmNaturalNumbers(args)
{
  // Loop Counters
  var k = 0;

  var directory = args.baselinepromptsdirectory
  var confValue = args.value;

  // Result object
  var prompts = new Object();
  prompts.audio = new Array();
  prompts.tts = new Array();

  // Getting the MEANING key if it is an object
  if (typeof(confValue) == 'object')
  {
    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
      confValue = confValue.MEANING;
    else
      confValue = confValue.dm_root;
  }

  // Converting type of confValue to String if is not "String"
  if (typeof(confValue) != "string")
  {
    confValue = confValue.toString();
  }

  // Verify if the number said is a negative number
  if (confValue.substr(0,1) == "-")
  {
    prompts.audio.push(directory + "/naturalnumbers/negative.ulaw");
    prompts.tts.push("negative");
    confValue = confValue.substr(1);
  }

  var integer;
  var decimal;

  if (confValue.indexOf(".") != -1)
  {
    var currArray = confValue.split('.')

    if (currArray.length == 2)
    {
      integer = currArray[0];
      decimal = currArray[1];
    }
    else
    {
      integer = 0;
      decimal = currArray[0];
    }
  }
  else
  {
    integer = confValue;
    decimal = "0";
  }

  var spellprompt = naturalNumber(integer);

  // Don't say things like 100-zero, so remove the last zero if it exists unless it is only one zero
  if ((spellprompt.tts[spellprompt.tts.length - 1] == '0') && (spellprompt.tts.length != 1))
  {
    for (k = 0; k < spellprompt.audio.length - 1; k++)
    {
      prompts.audio.push(directory + spellprompt.audio[k]);
      prompts.tts.push(spellprompt.tts[k]);
    }
  }
  else
  {
    for (k = 0; k < spellprompt.audio.length; k++)
    {
      prompts.audio.push(directory + spellprompt.audio[k]);
      prompts.tts.push(spellprompt.tts[k]);
    }
  }

  // Only play the decimal part if different from 0
  if (parseInt(decimal, 10) > 0)
  {
    // Play "point"
    prompts.audio.push(directory + "naturalnumbers/point.ulaw");
    prompts.tts.push("point");

    var toalphanum = new Object();
    toalphanum.baselinepromptsdirectory = directory;
    toalphanum.value = decimal;

    var alphanumprompt = confirmAlphanum(toalphanum);

    for (k = 0; k < alphanumprompt.audio.length; k++)
    {
      prompts.audio.push(alphanumprompt.audio[k]);
      prompts.tts.push(alphanumprompt.tts[k]);
    }
  }

  return prompts;
}

/*******************************************
* confirmPhone
*******************************************/
function confirmPhone(args)
{
  // Loop Counters
  var j = 0;
  var d = 0;

  var directory = args.baselinepromptsdirectory+"cardinal/";
  // Choose meaning for a builtin
  var confValue = args.value;

  // Result object
  var prompts = new Object();
  prompts.audio = new Array();
  prompts.tts = new Array();

  //Getting the MEANING key if it is an object
  if (typeof confValue == 'object')
  {
    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
      confValue = confValue.MEANING;
    else
      confValue = confValue.dm_root;
  }

  var phonenumber = confValue;
  var extension = "";
  var extIndex = confValue.indexOf("x");
  var spellprompt;

  // Splitting phone number and the extension for confirmation
  if (extIndex != -1)
  {
    phonenumber = confValue.substring(0, extIndex);
    extension = confValue.substring(extIndex + 1);
  }

  if (phonenumber.length == 11 && phonenumber.charAt(0) == "1")
  {
    prompts.audio.push(directory + "1.ulaw");
    prompts.tts.push("1");

    phonenumber = phonenumber.substring(1);
  }

  // Confirming the leading "1" as the country code and 800 as "8 hundred"
  if (phonenumber.length == 10)
  {
    if (args.raw.substring(0, 1) == "1" || args.raw.toUpperCase().substring(0, 3) == "ONE")
    {
      prompts.audio.push(directory + "1.ulaw");
      prompts.tts.push("1");
    }

    if (phonenumber.substring(0, 3) == "800")
    {
      spellprompt = naturalNumber("800");

      for (j = 0; j < spellprompt.audio.length; j++)
      {
        prompts.audio.push(args.baselinepromptsdirectory + spellprompt.audio[j]);
        prompts.tts.push(spellprompt.tts[j]);
      }

      phonenumber = phonenumber.substring(3);
    }
  }

  // Break into an array of digits
  var digits = phonenumber.match(/\d/g);

  for (d = 0; d < digits.length; d++)
  {
    prompts.audio.push(directory + digits[d] + ".ulaw");
    prompts.tts.push(digits[d]);
    if ((digits.length==10) && ((d == 2) || (d == 5)))
    {
      // Include pause
      prompts.audio.push(directory + "silence500ms.ulaw");
      prompts.tts.push(",");
    }
  }

  // Confirming the extension
  if (extension != null && extension.length > 0)
  {
    prompts.audio.push(directory + "/extension.ulaw");
    prompts.tts.push("extension");

    args.value = extension;
    spellprompt = confirmAlphanum(args);

    for (j = 0; j < spellprompt.audio.length; j++)
    {
      prompts.audio.push(spellprompt.audio[j]);
      prompts.tts.push(spellprompt.tts[j]);
    }
  }

  return prompts;
}

/*******************************************
* confirmSocialSecurity
*******************************************/
function confirmSocialSecurity(args)
{
  // Loop Counters
  var i = 0;
  var j = 0;

  // Confirmation arguments
  var directory = args.baselinepromptsdirectory;
  var confValue = args.value;

  // Create prompts object
  var prompts = new Object();
  prompts.audio = new Array();
  prompts.tts = new Array();

  //Getting the MEANING key if it is an object
  if (typeof confValue == 'object')
  {
    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
      confValue = confValue.MEANING;
    else
      confValue = confValue.dm_root;
  }

  var numbers = new Array();
  numbers.push(confValue.substr(0, 3));
  numbers.push(confValue.substr(3, 2));
  numbers.push(confValue.substr(5, 4));

  for (i = 0; i < numbers.length; i++)
  {
    var toalphanum = new Object();
    toalphanum.value = numbers[i];
    toalphanum.baselinepromptsdirectory = directory;
    var spellprompt = confirmAlphanum(toalphanum);

    for (j = 0; j < spellprompt.audio.length; j++)
    {
      prompts.audio.push(spellprompt.audio[j]);
      prompts.tts.push(spellprompt.tts[j]);
    }

    if (i < (numbers.length - 1))
    {
      prompts.audio.push(directory + "silence500ms.ulaw");
      prompts.tts.push("missing pause prompt");
    }
  }

  return prompts;
}

/*******************************************
* confirmTime
*******************************************/
function confirmTime(args)
{
  var directory = args.baselinepromptsdirectory;
  // Choose meaning for a builtin
  var confValue = args.MEANING;

  // Prompts object
  var prompts = new Object();
  prompts.audio = new Array();
  prompts.tts = new Array();

  //Getting the MEANING key if it is an object
  if (typeof confValue == 'object')
  {
    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
      confValue = confValue.MEANING;
    else
      confValue = confValue.dm_root;
  }

  if (confValue.length != 5)
  {
    prompts.audio.push(directory + confValue + ".ulaw");
    prompts.tts.push(confValue);
    return prompts;
  }

  // Parse the value string HHMM
  var hour = Math.round(confValue.substr(0, 2));
  var minute = confValue.substr(2, 2);
  var time = confValue.substr(0, 4);
  var fifthChar = confValue.substr(4, 5);
  var qualifier = args.value.QUALIFIER;

  if (fifthChar == "h" || fifthChar == "?")
  {
    if (time < 0 || time > 2400)
    {
      prompts.audio.push(directory + confValue + ".ulaw");
      prompts.tts.push(confValue);
      return prompts;
    }
  }
  else if (fifthChar == "a" || fifthChar == "p")
  {
    if (time < 0 || time > 1259)
    {
      prompts.audio.push(directory + confValue + ".ulaw");
      prompts.tts.push(confValue);
      return prompts;
    }
  }

  //Playing qualifier if necessary : approximately, before, after
  if (qualifier != null)
  {
    if (qualifier == "approx")
    {
      prompts.audio.push(directory + "time/approximately.ulaw");
      prompts.tts.push("approximately");
    }
    else if (qualifier == "before")
    {
      prompts.audio.push(directory + "time/before.ulaw");
      prompts.tts.push("before");
    }
    else if (qualifier == "after")
    {
      prompts.audio.push(directory + "time/after.ulaw");
      prompts.tts.push("after");
    }
  }

  //Playing the hour
  prompts.audio.push(directory + "cardinal/"+hour+".ulaw");
  prompts.tts.push(hour);

  if (fifthChar == "h")
  {
    prompts.audio.push(directory + "cardinal/hundred.ulaw");
    prompts.tts.push("hundred");
  }

  //Playing minutes if needed
  if (minute != "00")
  {
    prompts.audio.push(directory + "cardinal/" + minute + ".ulaw");
    prompts.tts.push(minute);
  }

  //Playing am, pm or hours if needed
  if (fifthChar == "a")
  {
    prompts.audio.push(directory + "time/am.ulaw");
    prompts.tts.push("a m");
  }
  else if (fifthChar == "p")
  {
    prompts.audio.push(directory + "time/pm.ulaw");
    prompts.tts.push("p m");
  }
  else if (fifthChar == "h")
  {
    prompts.audio.push(directory + "time/hours.ulaw");
    prompts.tts.push("hours");
  }

  return prompts;
}

/*******************************************
* confirmZipCode
*******************************************/
function confirmZipCode(args)
{
  // Loop Counters
  var i = 0;
  var j = 0;

  var directory = args.baselinepromptsdirectory;
  // Choose meaning for a builtin
  var confValue = args.MEANING;

  // Prompts object
  var prompts = new Object();
  prompts.audio = new Array();
  prompts.tts = new Array();

  //Getting the MEANING key if it is an object
  if (typeof confValue == 'object')
  {
    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
      confValue = confValue.MEANING;
    else
      confValue = confValue.dm_root;
  }

  var zipcode;
  zipcode = confValue.substr(0, 5);

  if (confValue.length == 9)
  {
    // NSRD00050286 - Only subset of "ZIP + 4" US zipcode is confirmed in OSDM zipcode
    zipcode = confValue.substr(0, 9);
  }

  var toalphanum = new Object();
  toalphanum.value = zipcode;
  toalphanum.baselinepromptsdirectory = directory;
  var spellprompt = confirmAlphanum( toalphanum );

  for (j = 0; j < spellprompt.audio.length; j++)
  {
    prompts.audio.push(spellprompt.audio[j]);
    prompts.tts.push(spellprompt.tts[j]);
  }

  return prompts;
}

/*******************************************
* naturalNumber
*******************************************/
// Produce an array of prompts for a natural number
// criminally underused here.
function naturalNumber(confValue)
{
  // Loop counters
  var i = 0;

  // Prompts object
  var prompts = new Object();
  prompts.audio = new Array();
  prompts.tts = new Array();

  //Getting the MEANING key if it is an object
  if (typeof(confValue) == 'object')
  {
    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
      confValue = confValue.MEANING;
    else
      confValue = confValue.dm_root;
  }

  var millions = Math.floor(confValue/1000000);
  var thousands = Math.floor((confValue-(millions*1000000))/1000);
  var hundreds = Math.floor((confValue-(millions*1000000)-(thousands*1000))/100);
  var tensOnes = confValue % 100;
  var prompt = 0;

  if (millions > 0)
  {
    var millionsResult = naturalNumber(millions);
    for (i = 0; i < millionsResult.audio.length; i++)
    {
     prompts.audio.push(millionsResult.audio[i]);
     prompts.tts.push(millionsResult.tts[i]);
    }

    // push units prompt
    prompts.audio.push("cardinal/million.ulaw");
    prompts.tts.push("million");
  }

  if (thousands > 0)
  {
    var thousandsResult = naturalNumber( thousands );
    for (i = 0; i < thousandsResult.audio.length; i++)
    {
      prompts.audio.push(thousandsResult.audio[i]);
      prompts.tts.push(thousandsResult.tts[i]);
    }
    // push units prompt
    prompts.audio.push("cardinal/thousand.ulaw");
    prompts.tts.push("thousand");
  }

  if (hundreds > 0)
  {
    var hundredsResult = naturalNumber(hundreds);
    for (i = 0; i < hundredsResult.audio.length; i++)
    {
      prompts.audio.push(hundredsResult.audio[i]);
      prompts.tts.push(hundredsResult.tts[i]);
    }
    // push units
    prompts.audio.push("cardinal/hundred.ulaw");
    prompts.tts.push("hundred");
  }

  if (tensOnes > 0)
  {
    prompts.audio.push("cardinal/" + tensOnes + ".ulaw");
    prompts.tts.push(tensOnes);
  }
  // Play '0' only if there is nothing else. It's to avoid "4 hundred 0" for '400'
  else if (millions == 0 && thousands == 0 && hundreds == 0 && tensOnes == 0)
  {
    prompts.audio.push("cardinal/" + tensOnes + ".ulaw");
    prompts.tts.push(tensOnes);
  }

  return prompts;
}


/*************************************************************
*  Repeat Prompts
*************************************************************/
function isCommand(args)
{
  var confValue = args.value;

  var isCommand = false;

  // Check if the value elemt is an object
  if (typeof confValue == 'object')
  {
    // Check to see if a command has been found
    if ((typeof(confValue.dm_command) != 'undefined') && (confValue.dm_command != null))
      isCommand = true;
  }

  return isCommand;
}

/*******************************************
* repeatAlphanum
*******************************************/
function repeatAlphanum(args)
{
  if (isCommand(args))
    return confirmCommand(args);

  return confirmAlphanum(args);
}

/*******************************************
* repeatCCExpDate
*******************************************/
function repeatCCExpDate(args)
{
  if (isCommand(args))
    return confirmCommand(args);

  return confirmCCExpDate(args);
}

/*******************************************
* repeatCreditCard
*******************************************/
function repeatCreditCard(args)
{
  if (isCommand(args))
    return confirmCommand(args);

  return confirmCreditCard(args);
}

/*******************************************
* repeatCurrency
*******************************************/
function repeatCurrency(args)
{
  if (isCommand(args))
    return confirmCommand(args);

  return confirmCurrency(args);
}

/*******************************************
* repeatCustomContext
*******************************************/
function repeatCustomContext(args)
{
  if (isCommand(args))
    return confirmCommand(args);

  return confirmCustomContext(args);
}

/*******************************************
* repeatDate
*******************************************/
function repeatDate(args)
{
  if (isCommand(args))
    return confirmCommand(args);

  return confirmDate(args);
}

/*******************************************
* repeatDigits
*******************************************/
function repeatDigits(args)
{
  if (isCommand(args))
    return confirmCommand(args);

  return confirmDigits(args);
}

/*******************************************
* repeatNaturalNumbers
*******************************************/
function repeatNaturalNumbers(args)
{
  if (isCommand(args))
    return confirmCommand(args);

  return confirmNaturalNumbers(args);
}

/*******************************************
* repeatPhone
*******************************************/
function repeatPhone(args)
{
  if (isCommand(args))
    return confirmCommand(args);

  return confirmPhone(args);
}

/*******************************************
* repeatSocialSecurity
*******************************************/
function repeatSocialSecurity(args)
{
  if (isCommand(args))
    return confirmCommand(args);

  return confirmSocialSecurity(args);
}

/*******************************************
* repeatTime
*******************************************/
function repeatTime(args)
{
  if (isCommand(args))
    return confirmCommand(args);

  return confirmTime(args);
}

/*******************************************
* repeatYesNo
*******************************************/
function repeatYesNo(args)
{
  if (isCommand(args))
    return confirmCommand(args);

  var prompts = new Object();
  prompts.audio = new Array();
  prompts.tts = new Array();

  return prompts;
}

/*******************************************
* repeatZipCode
*******************************************/
function repeatZipCode(args)
{
  if (isCommand(args))
    return confirmCommand(args);

  return confirmZipCode(args);
}
