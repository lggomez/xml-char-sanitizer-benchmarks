using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;

namespace XmlBenchmark
{
    [HtmlExporter, CsvExporter, MarkdownExporter]
    public class XmlStringSanitizer
    {
        #region string setup

        public readonly string StringMessage = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam rutrum et urna sollicitudin pellentesque. Suspendisse in commodo dolor. Maecenas consequat leo vitae lectus semper viverra. Nulla nec pretium diam. Integer scelerisque accumsan dui eget faucibus. Aliquam aliquet iaculis euismod. Vestibulum a dictum leo, et sodales libero.

Aenean hendrerit lectus id ex porta tincidunt. Donec sagittis pulvinar ipsum, ut scelerisque sapien feugiat at. Donec at mattis turpis. Praesent sollicitudin tincidunt tortor, eu consectetur odio dictum a. Suspendisse turpis odio, convallis non sollicitudin et, egestas a purus. Ut lorem neque, porta vitae dolor vitae, sodales tempor orci. Quisque id nibh a tellus consectetur fringilla. Fusce pharetra dictum ipsum a lobortis. Sed gravida nisl sed tortor consequat lacinia.

Morbi nec aliquam ex, at rutrum purus. Nulla facilisi. In sed varius orci. Nam nec nunc at massa vestibulum convallis nec in orci. In et lectus sapien. Sed dui nisi, cursus vel libero id, egestas iaculis leo. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed eleifend sodales mi, in ullamcorper neque tincidunt at. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis hendrerit, quam sit amet blandit malesuada, dolor orci mattis tortor, eu facilisis magna leo id elit. Sed aliquet porta elit, eu consectetur velit vulputate vel.

Phasellus in commodo augue. Sed vel finibus justo. Nam eget interdum neque. Donec eget ornare ex. Aliquam mollis malesuada pharetra. Pellentesque luctus eros vitae purus convallis, sit amet cursus dolor tristique. Etiam lacus nulla, cursus a viverra sit amet, tristique id mi. Suspendisse a augue nulla. Aliquam sit amet mauris felis. Nunc lorem ante, lacinia vel ligula id, sodales interdum arcu. Nunc sed nisl neque. Praesent eu lectus nunc. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.

Morbi dapibus, sem vel tristique tempor, nunc purus tristique ex, ut faucibus purus ante in dolor. Morbi vitae venenatis est. Fusce sit amet efficitur ante. Suspendisse accumsan neque vulputate nibh auctor lacinia. Suspendisse imperdiet placerat sapien, eu consequat tortor semper at. Nunc sed velit egestas, scelerisque augue ac, ultricies tellus. In gravida, ligula a facilisis aliquet, justo nunc rutrum tortor, nec tempor lectus quam a nulla.

Nam elementum venenatis urna at iaculis. Nam sed porta risus. Donec pharetra ac felis id auctor. Phasellus lacinia maximus lacus, ut rutrum felis ullamcorper in. Suspendisse tincidunt diam dapibus tempus fringilla. Quisque ut mi quam. Nunc tristique posuere sodales. Donec nec est nec justo molestie lobortis in feugiat turpis.

Donec ex mi, venenatis sit amet pretium venenatis, condimentum nec felis. Sed diam urna, tempor et pulvinar vel, ornare eget orci. Maecenas ut elit a lacus dictum iaculis a a mauris. Nullam suscipit iaculis libero sed aliquet. Nulla sagittis pulvinar vehicula. Morbi hendrerit sed lorem eget gravida. Phasellus malesuada, enim a posuere tincidunt, nisl velit facilisis ex, sed maximus sem felis sed nisi.

Fusce ipsum lectus, consequat ut orci eget, bibendum scelerisque neque. Donec luctus congue odio. Integer quis arcu diam. Nullam volutpat porttitor porttitor. Aenean ac sodales augue, at vestibulum ipsum. Donec at tincidunt nisl. Fusce venenatis, leo at fringilla dictum, massa justo feugiat mauris, in placerat nisi augue in ligula. Cras vitae laoreet leo. Integer eleifend, lorem tincidunt lacinia pellentesque, nisi justo luctus quam, sed euismod erat ipsum vel mauris. Vestibulum ultricies ligula id sapien tempus luctus. Ut facilisis nulla quis nulla eleifend, vel volutpat tortor ornare. Praesent tincidunt at ligula at consequat. Maecenas ultrices libero ut tortor faucibus, ut suscipit enim rutrum.

Aenean malesuada at odio nec convallis. Aenean vel accumsan dolor. Phasellus at velit id justo cursus feugiat eget id purus. Donec accumsan eget nunc nec scelerisque. Quisque a egestas nisl, sit amet lobortis eros. Vivamus vitae malesuada sem, et rhoncus quam. Cras condimentum eros erat, eget mollis odio viverra ultricies. Proin quis nisi dignissim, dictum quam in, varius massa. Vestibulum pretium, diam non tristique tincidunt, nisi nibh dapibus metus, nec sollicitudin augue ante eu tellus. Quisque id nisl euismod, sagittis nibh id, porttitor nisi. Aenean dapibus ipsum vitae erat tempus, eu fringilla urna condimentum. Phasellus sodales dolor vel vulputate mattis. Vestibulum vel orci tristique, interdum quam in, scelerisque purus. Aenean venenatis nunc imperdiet tellus laoreet, et feugiat risus commodo.

Quisque tincidunt dolor dictum blandit semper. Vivamus ornare feugiat sem. Etiam egestas non libero in vehicula. Praesent vel efficitur nisi. Phasellus in ex hendrerit turpis tempus rhoncus. Nulla ultrices accumsan quam non semper. Integer fermentum nisl risus, non euismod risus sagittis ac. Nulla ut consectetur urna, quis aliquet nibh. Nulla molestie imperdiet nisl, non suscipit mi cursus eget. Nam fermentum aliquam ex, eget sollicitudin enim venenatis venenatis. Aenean et lorem et neque pharetra blandit. Ut eu convallis arcu. Sed ultrices faucibus iaculis. Ut accumsan ligula sit amet convallis scelerisque. Etiam molestie tellus non orci ultrices, ac dictum magna viverra.

Morbi pharetra augue id gravida vulputate. Curabitur molestie magna vel condimentum egestas. Ut maximus, nibh et ultricies pulvinar, ipsum ex cursus metus, vel iaculis metus tortor et lorem. Morbi vestibulum metus non iaculis eleifend. Duis magna diam, feugiat faucibus commodo quis, aliquam vitae arcu. Praesent tortor lectus, convallis nec eros eu, suscipit sollicitudin diam. Sed sit amet massa ligula. Aliquam erat volutpat. Suspendisse gravida vulputate ligula, in laoreet eros consectetur at. Morbi eget venenatis enim. Aliquam a risus est.

Mauris ullamcorper ligula interdum turpis tristique sollicitudin. Nunc libero est, posuere facilisis tortor a, condimentum convallis mauris. Integer non porta sem. Pellentesque interdum volutpat elit vitae vulputate. Integer ipsum orci, pharetra ac eleifend ut, euismod in felis. Etiam vel dignissim lacus, id mollis elit. Suspendisse sodales, turpis a laoreet accumsan, ex justo posuere eros, laoreet fringilla justo magna sit amet mauris. Etiam a ante lectus. Maecenas faucibus faucibus lobortis. In justo dui, malesuada ut nulla in, malesuada vulputate ipsum. Suspendisse ac mauris vitae lacus feugiat imperdiet a sed ipsum. Suspendisse varius sapien ac est viverra, varius pellentesque tellus cursus. Aliquam dapibus tincidunt erat, a porta quam. Mauris ultrices mauris quis massa aliquam fringilla. Aenean ac sem eget dolor accumsan molestie.

Cras sed tellus quis tortor commodo scelerisque ut vitae dolor. Praesent quis nunc in turpis viverra tempus id in lectus. Etiam tincidunt diam non nulla laoreet faucibus. Sed risus tellus, venenatis vitae enim id, aliquet ultricies nunc. Vivamus non leo vel sapien pulvinar condimentum. Etiam vel arcu quis mi lacinia rhoncus. In ligula est, molestie quis iaculis sit amet, mollis ac ligula. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed tempus nunc ac cursus lobortis. Vestibulum quis odio est. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Vestibulum ut fermentum odio, sit amet ultrices leo.

Cras eget metus sed elit ultricies egestas. Morbi sit amet ipsum a tellus finibus congue at sit amet sapien. Curabitur in sem eget magna tincidunt blandit. Nulla ultrices odio vel lectus iaculis, in consectetur risus sollicitudin. Morbi lobortis lorem quis hendrerit mattis. Curabitur placerat fermentum leo, vitae auctor mauris elementum sed. Etiam accumsan magna vitae aliquet elementum. Mauris fringilla risus quam. Nam eu nisl mollis, cursus orci eget, varius dui. Praesent ac congue nisl. Aenean tristique aliquet diam scelerisque ultrices. In a tincidunt dolor. Vestibulum sit amet consequat quam.

Ut at lobortis lacus, pulvinar posuere quam. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Ut non lacus in eros vulputate faucibus. Quisque ipsum tellus, finibus eget vehicula sit amet, sagittis vel sapien. Interdum et malesuada fames ac ante ipsum primis in faucibus. Phasellus eu accumsan ante. Praesent ultrices ligula ligula, ac posuere orci viverra sed. Donec posuere, orci a tempus pellentesque, magna nisi tempor tortor, sagittis lobortis purus eros sodales lacus. Pellentesque facilisis nulla sit amet tristique vehicula. Proin efficitur laoreet arcu sit amet sollicitudin.

Curabitur viverra, ex ut aliquam placerat, urna ipsum sollicitudin diam, sed convallis ex nisl ac est. Maecenas sapien lacus, sollicitudin vitae augue a, egestas semper arcu. Suspendisse in pretium felis. Nunc vel nulla ac dolor facilisis maximus ut eu elit. Morbi sit amet lectus at libero mattis semper. Aenean faucibus, ipsum nec ullamcorper tempor, lectus nisi malesuada quam, sed cursus nisl leo at magna. Sed efficitur neque id varius porta. Phasellus congue luctus ultricies.

Cras posuere, ante non faucibus lobortis, ligula nulla faucibus felis, a fermentum elit orci ut nunc. Aliquam consequat at urna porta viverra. In non diam porttitor neque rhoncus posuere ac ac mi. In hac habitasse platea dictumst. Phasellus ut viverra augue. Quisque facilisis ex ac nisi faucibus ornare. Nam at nisl ultricies, elementum ipsum vel, dignissim leo. Donec consequat mauris nec semper molestie. Vestibulum sit amet lorem eget erat accumsan vestibulum. Aliquam id magna sit amet ligula sodales ullamcorper. Mauris venenatis faucibus semper. Nam vitae orci a dolor eleifend accumsan. Cras non eros commodo, semper massa at, maximus mi.

Integer molestie viverra eros, in accumsan lacus iaculis sit amet. Aliquam iaculis imperdiet sagittis. Fusce scelerisque, lectus ut scelerisque mattis, elit urna molestie mauris, eu rutrum augue tortor a lorem. Vivamus ac mauris nunc. Ut vitae luctus dui, at tempor est. Pellentesque arcu lorem, vulputate tincidunt hendrerit eget, vestibulum vitae velit. Mauris sem arcu, ultricies quis lacus sed, malesuada accumsan sapien. Proin sit amet venenatis dui. Curabitur augue augue, consequat quis feugiat ac, suscipit nec mauris. Ut lorem diam, congue vitae enim vel, elementum finibus justo. Sed aliquet erat eu pellentesque malesuada. Vestibulum sit amet leo scelerisque, ornare nisl sed, malesuada sapien. Mauris maximus, ligula id facilisis tristique, felis enim convallis lectus, non convallis erat augue quis enim.

Aliquam ac commodo lacus. Nam ut suscipit urna. Nunc at sapien lacus. Aliquam in dolor eget mauris imperdiet blandit eget et urna. Nam mi orci, venenatis a neque vel, lacinia ullamcorper lectus. Quisque rhoncus magna vitae dignissim tincidunt. Donec nec pharetra risus. Nam dictum sit amet libero vel luctus. Sed aliquet felis a nibh tristique commodo. Aenean lobortis vestibulum neque vitae lacinia.

Integer semper eu risus non convallis. Mauris mollis mollis justo non luctus. Nam mattis efficitur dui, vitae tempus erat mollis quis. Nulla vel quam sed velit placerat blandit ut in velit. Phasellus hendrerit purus nec nisi consectetur, at feugiat dolor ultricies. Phasellus pharetra auctor odio, in fermentum est dictum in. Vestibulum mattis condimentum finibus. Morbi quis blandit massa. Aliquam erat volutpat. Cras ipsum risus, tristique ac ligula sed, consectetur semper neque. Etiam ullamcorper pharetra felis, vitae aliquam lacus faucibus in.

Sed non urna ac purus pharetra pretium. Nullam porttitor fringilla erat, eu facilisis lorem faucibus interdum. Vivamus ut felis ipsum. Aliquam consectetur congue odio, sit amet porta dolor efficitur quis. Etiam faucibus eleifend ipsum, vitae pharetra tellus tempus at. Integer blandit velit sed ipsum ullamcorper blandit. Donec dapibus nunc eu hendrerit lacinia. Donec ultrices, ante quis molestie pharetra, lacus urna gravida justo, rutrum suscipit enim sem ac tellus. Suspendisse elit libero, interdum eu posuere quis, consequat sit amet nunc. Nulla at dignissim leo. Cras eget malesuada libero. Donec ac tellus pellentesque, maximus metus id, maximus neque. Morbi ut fringilla arcu, nec vestibulum justo.

Fusce egestas ex id semper efficitur. Mauris magna nibh, malesuada nec arcu quis, sodales finibus nisi. Nullam vestibulum turpis eu vulputate sagittis. Mauris faucibus leo vel sapien maximus bibendum. Praesent euismod sit amet purus eu mollis. Duis vestibulum, lectus sed aliquam hendrerit, tortor libero luctus augue, eu congue metus quam feugiat ex. In non ultrices massa. Suspendisse sollicitudin ligula sollicitudin odio efficitur, sit amet finibus velit luctus. Nullam auctor id sem quis hendrerit. Vestibulum gravida felis vel elementum finibus. Morbi gravida sem sed libero efficitur, at tincidunt risus lobortis. Vivamus sed quam metus. Maecenas ultricies ornare ante, ac malesuada felis condimentum sed. Etiam dapibus enim sapien. Maecenas id rhoncus purus. Nulla vel tellus lectus.

Quisque convallis, urna et lacinia sollicitudin, ligula ante gravida ligula, sed auctor nisi ex convallis erat. Donec auctor maximus massa, eu egestas orci faucibus ut. Cras congue euismod justo, sit amet ultrices sapien condimentum vitae. Proin scelerisque erat sed neque mollis, eget pretium mauris mattis. Integer ut sem lacinia, bibendum felis vel, molestie lorem. Duis ullamcorper luctus nibh vel eleifend. Curabitur facilisis volutpat odio, non commodo arcu tristique at. Sed in nulla et sapien consequat tempor. Morbi vehicula consectetur dolor a venenatis. Aenean pharetra, est quis hendrerit mattis, urna quam rhoncus urna, nec suscipit nisi magna sed eros. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Fusce ac enim id purus sollicitudin auctor.

Phasellus in tempor sem, a condimentum ipsum. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Fusce ac mollis augue. Maecenas aliquam ante ut sem pellentesque dignissim. Vestibulum sit amet leo aliquam libero blandit bibendum. Donec bibendum arcu sit amet lorem imperdiet tristique. Sed laoreet mauris vitae orci mollis, in viverra mauris vestibulum.

Nullam commodo leo ante. Suspendisse laoreet velit ligula, eget malesuada nisl finibus ac. Cras blandit mauris ut consectetur luctus. Duis et molestie neque. Donec id finibus orci, a scelerisque nulla. Aliquam erat volutpat. Nulla erat eros, dictum feugiat pellentesque egestas, tincidunt id felis. Proin feugiat mattis arcu, vitae convallis diam tincidunt sed.

Etiam tincidunt tristique nunc, sed varius purus gravida porta. Vivamus ultrices tellus vel erat hendrerit sodales. Aliquam ut ligula nec metus efficitur hendrerit sed in est. Aenean maximus, felis ac aliquet molestie, enim purus volutpat neque, sit amet mollis orci justo quis lacus. Sed fermentum luctus ultrices. Vestibulum mollis est et dolor ultricies, eu posuere enim volutpat. Nulla consectetur, nisl id congue bibendum, metus velit malesuada elit, sed congue magna turpis quis dolor. Curabitur quis risus laoreet, tempus quam eget, vehicula enim. Morbi tincidunt lobortis massa a placerat.

Proin neque leo, fermentum eu bibendum sed, porttitor at felis. Phasellus vulputate dolor sit amet tellus posuere porttitor vitae at dolor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas tincidunt, mi in bibendum lobortis, mauris ex varius risus, in aliquam nibh dolor vitae erat. Fusce nec placerat erat. Integer at mi nec justo interdum condimentum vel et ipsum. Nulla auctor nec justo ut tristique. Etiam tortor purus, egestas eu pellentesque a, suscipit sed diam. Maecenas lacus nibh, ultrices a rhoncus in, porttitor eget turpis. Sed quis orci tempus, rutrum odio porttitor, semper purus.

Morbi non diam at leo eleifend euismod. Donec sodales orci purus, vel mattis sem fringilla non. Sed accumsan, sem sed fermentum eleifend, purus neque viverra nisi, sit amet interdum est ipsum sed nisi. Quisque laoreet orci eget varius aliquet. Fusce ultrices ante odio, id pulvinar lorem maximus at. Integer at leo tellus. Sed vulputate purus non magna viverra consequat. Praesent bibendum lorem mattis libero finibus, ut dignissim elit cursus. Duis eleifend lorem sit amet nulla volutpat, quis laoreet est bibendum. Quisque euismod pretium suscipit. Sed a facilisis nulla.

Cras at ante efficitur, scelerisque metus quis, tincidunt sem. Aenean id mollis felis. Fusce non dolor dignissim, ullamcorper dolor sed, rutrum neque. In placerat laoreet tincidunt. Mauris consectetur at velit id sodales. Ut ornare metus vel elit condimentum, eget rhoncus augue lobortis. Etiam enim nibh, vehicula eget nulla ut, convallis hendrerit felis. Morbi suscipit non quam vitae congue. Morbi consectetur eros nulla, quis pulvinar risus rutrum at. Sed dictum, ligula vel vestibulum faucibus, sapien mi imperdiet velit, nec lobortis nibh ex laoreet urna. Duis at neque at tortor pulvinar ornare a eu lectus.

Ut ullamcorper eros vel libero consequat, vel tempor arcu consectetur. Fusce mi erat, feugiat ac eros nec, finibus dictum urna. Sed blandit egestas magna eget euismod. Maecenas in sagittis mauris. In augue nisi, ornare eu dui vitae, interdum aliquam mi. Interdum et malesuada fames ac ante ipsum primis in faucibus. In vitae ultrices leo. Vivamus viverra rutrum varius. Praesent ullamcorper ultricies cursus. Cras tincidunt, risus et hendrerit rhoncus, mauris dui dignissim turpis, pellentesque accumsan quam urna vel turpis. Vestibulum enim nisl, eleifend vel lacus nec, molestie interdum dolor. Proin ut est urna. Vestibulum vel neque ultricies, viverra erat in, posuere neque.

Pellentesque cursus erat vitae nunc ultricies pellentesque. Nullam et tristique metus, eu suscipit sapien. Nunc at tempus ligula. Quisque nec felis aliquet, faucibus nulla quis, pharetra turpis. Integer sodales mi eros. Sed placerat mi ut lacus tempus, sit amet pulvinar enim aliquet. Sed lacinia, velit id malesuada feugiat, libero metus tempus magna, non mattis sapien eros sit amet odio. Ut nec magna non lorem dictum consequat. Aliquam erat volutpat. Mauris in pulvinar libero. Donec mattis lacus velit, id posuere erat semper quis. Maecenas nunc felis, maximus sed neque nec, dignissim consectetur velit. Proin in pretium velit, sed ultrices nisi. Nunc faucibus nulla eu odio fermentum, id aliquam mi tristique.

Duis odio neque, vulputate id metus sit amet, congue maximus lectus. Fusce accumsan in ligula iaculis malesuada. In porttitor velit at libero luctus euismod. Aliquam sit amet nisl quam. Suspendisse aliquam diam ipsum, vitae pulvinar est gravida sit amet. Ut consectetur augue in malesuada egestas. Sed tincidunt semper magna, ut sollicitudin nulla blandit at. Cras ut dictum augue. Proin et augue elit. Maecenas enim turpis, hendrerit et luctus sit amet, mollis sit amet mauris. Aenean massa leo, varius nec mi nec, iaculis efficitur turpis. Sed porttitor dolor scelerisque ex gravida, id laoreet mauris sollicitudin. Quisque blandit egestas vestibulum.

Nulla in lacus sit amet sem blandit luctus. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Suspendisse at augue ligula. Duis imperdiet et nibh sed posuere. Cras eget nunc nec eros sollicitudin suscipit eget ac justo. Vivamus ac est at risus pulvinar dapibus vitae vitae augue. Etiam fermentum elit aliquam sapien commodo lobortis. Integer maximus eget quam ut scelerisque. Proin finibus massa tincidunt ex interdum, ac mollis urna molestie.

Integer tempor nisi ligula, sit amet suscipit nisi lobortis non. Nam congue sed metus at tristique. Ut congue elit non lacus consequat eleifend. Cras mattis semper justo, eget fringilla ligula dapibus id. Morbi sit amet auctor nisi. Morbi id diam nec elit pretium eleifend non ut augue. Phasellus hendrerit massa a ipsum tempus dapibus. Cras nec nisl elit. Vestibulum eu ipsum vehicula, malesuada ex ut, scelerisque tortor. Nunc egestas hendrerit mi nec porttitor. Etiam eget eleifend metus. Aliquam dapibus enim ante, quis auctor dolor imperdiet id.

Suspendisse faucibus urna non erat suscipit, et tempus leo condimentum. Nunc et dolor vehicula purus consectetur porttitor nec vitae nulla. Phasellus scelerisque libero iaculis, suscipit lacus at, viverra erat. Ut vulputate euismod leo tempor vestibulum. Mauris in dolor mauris. Etiam vel egestas arcu. Nunc sit amet tortor sapien. Praesent sit amet iaculis neque, vitae gravida diam. Morbi mi urna, fermentum egestas ligula et, rutrum semper lacus. Nunc sed turpis ultrices, convallis magna a, imperdiet diam.

Sed eu est a odio ullamcorper tempus quis quis est. Fusce fringilla ligula sed tellus posuere consectetur. Pellentesque a mattis augue, eu sollicitudin tortor. Sed eget tortor a orci hendrerit faucibus. Aenean a ultricies odio. Suspendisse id convallis leo. Morbi euismod dapibus ipsum ac suscipit. Nullam imperdiet quam vitae ex posuere, vel tempus enim lacinia. Quisque nunc nisi, lacinia quis pellentesque sed, suscipit vitae quam. Nulla vel ligula sit amet est tincidunt tincidunt. Etiam quis lacus lacinia, dignissim ipsum sit amet, egestas urna. Vestibulum vitae consectetur tellus. Aliquam luctus posuere egestas. Curabitur sed ultricies enim. Donec sit amet egestas felis. Pellentesque eget consequat justo.

Etiam nisi diam, feugiat eget dapibus non, efficitur ac purus. Vivamus vestibulum nisl ultricies eleifend congue. Quisque euismod urna purus, sit amet viverra tellus sollicitudin quis. Praesent sed placerat turpis, sit amet dictum nulla. Nullam tincidunt eros a velit tincidunt pulvinar. Curabitur facilisis odio vitae nisi viverra, pellentesque venenatis mi rhoncus. Cras neque eros, euismod vel erat eu, laoreet euismod libero. Aliquam erat volutpat. Suspendisse a lectus tristique, sollicitudin justo vel, viverra leo.

Nam hendrerit metus orci. Proin leo eros, blandit vel sollicitudin at, sagittis vitae metus. Maecenas erat erat, dapibus et suscipit eget, commodo quis elit. Morbi varius tellus in nisl maximus laoreet sit amet in diam. Proin maximus, leo a semper malesuada, neque lorem rutrum enim, pellentesque facilisis libero dui ac velit. In urna purus, consequat vel dapibus at, porta eget ex. Vivamus luctus dapibus metus id placerat. Ut quis scelerisque nunc. Nulla felis eros, bibendum facilisis purus quis, feugiat fermentum est.

Praesent ac consectetur nibh. Vivamus at auctor nisi, ac lacinia nunc. Ut ultrices ultrices lectus in maximus. Maecenas ornare nulla gravida elit tincidunt porttitor. Fusce efficitur orci augue, sit amet eleifend lectus pretium id. Vestibulum ac odio commodo nisl dignissim feugiat. Morbi non sollicitudin massa. Quisque semper sem et justo fermentum, vitae scelerisque tellus commodo.

Nullam vel tortor eu dolor venenatis sodales vel quis sem. Quisque faucibus iaculis pellentesque. Sed vitae feugiat sapien. Aliquam condimentum tortor in ex scelerisque vestibulum. Donec nec odio lacinia, aliquet mauris in, maximus lorem. Cras posuere tempor metus id luctus. Phasellus mollis libero varius ipsum rutrum, sed hendrerit felis luctus. Donec tincidunt, ipsum ac pulvinar feugiat, justo neque vestibulum urna, quis gravida sem tellus vitae arcu. Aliquam scelerisque nunc a iaculis ultricies. Sed efficitur, urna et blandit eleifend, nulla metus finibus orci, a fringilla sapien lorem non mauris. Proin quis ante vitae tortor maximus mollis sit amet vitae felis. Fusce vestibulum vestibulum enim nec lacinia.

Nunc pretium ornare arcu, ut sodales libero feugiat eu. Maecenas in gravida tortor, eu feugiat orci. Aliquam egestas diam ut dapibus pellentesque. Morbi leo justo, laoreet quis massa id, vestibulum faucibus lectus. Integer nec nisl consectetur, vehicula purus a, faucibus dui. Duis eget velit vitae erat bibendum scelerisque ut sed tellus. Nam diam mi, tincidunt eget rhoncus quis, malesuada facilisis mi. In bibendum lacus sed orci interdum pharetra. Quisque vulputate risus lectus, eleifend volutpat enim malesuada et.

Ut feugiat est tortor, ac blandit leo posuere ac. Suspendisse suscipit auctor enim sit amet bibendum. Praesent vitae tortor in est consequat pretium vitae eget nibh. Proin scelerisque iaculis finibus. Mauris pretium nisl turpis, sit amet accumsan quam suscipit non. Nam ut blandit nulla. Nunc fermentum vel ante et fringilla. Nullam dignissim quis elit nec porta. Donec felis nulla, luctus ac diam sit amet, consequat sollicitudin risus. Donec vestibulum in libero id volutpat. Donec dignissim, ante id pulvinar hendrerit, erat mi condimentum ex, et eleifend justo sapien sit amet nibh. Phasellus sollicitudin ligula sed erat mollis, a iaculis ligula tristique. Sed sed rutrum leo.

Maecenas nec lacus tristique, auctor tortor ut, venenatis ante. Aenean sed semper ligula. In commodo tempor lorem ut egestas. Phasellus cursus dictum nisi vitae iaculis. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam sapien nulla, tincidunt sit amet ante vitae, efficitur auctor massa. Donec malesuada odio quis augue auctor vehicula. Nunc ullamcorper mi eu mattis vulputate.

Fusce ut turpis bibendum metus pretium feugiat id eu mi. Sed ultrices porta dui ac volutpat. Donec vel tortor maximus odio mollis gravida. Suspendisse quis nulla sed dolor luctus varius. Quisque lorem neque, sodales sed elit at, tincidunt scelerisque urna. Sed efficitur et metus dignissim dapibus. Pellentesque lectus mauris, pharetra non tortor eu, euismod vulputate erat. Nullam sit amet egestas ex. Etiam faucibus dictum odio nec euismod.

Praesent consequat neque non neque egestas, sit amet aliquet lectus vehicula. Nam euismod leo quis feugiat feugiat. Suspendisse lobortis bibendum velit, ac faucibus erat pellentesque sed. In et purus id purus fringilla scelerisque id sit amet velit. Donec in lobortis ipsum. Nam pellentesque eros eget dictum elementum. Vestibulum in suscipit arcu, eu viverra sapien. Nunc sodales ornare nisl, a suscipit ante posuere sed. Donec sollicitudin commodo scelerisque.

Nulla quis vehicula nunc, maximus fermentum felis. Nulla at elementum neque, sit amet sollicitudin mi. Vivamus malesuada turpis nibh, sed interdum tortor tempus sed. Duis massa lorem, ultrices sit amet tempus eget, tempus id turpis. Suspendisse potenti. Nam at neque et nulla dignissim molestie. Sed fringilla posuere felis, ac venenatis ante facilisis eget. Praesent non dolor ipsum. Mauris fringilla convallis sem nec porta. Suspendisse eget nunc eu est laoreet imperdiet. Vestibulum tincidunt efficitur efficitur. In nec molestie neque, molestie efficitur sem. Donec aliquet rutrum sem nec commodo. Proin convallis ipsum erat, sit amet faucibus urna congue vel.

Nulla congue est mi, eu lobortis nisl consequat vel. Proin auctor facilisis ante, sed feugiat lectus rutrum ut. Vivamus turpis leo, sollicitudin in mollis a, pulvinar quis nibh. Donec lobortis libero molestie, ultricies libero nec, pellentesque augue. Mauris eros quam, dignissim aliquet elementum sed, tempus ut mauris. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nullam ut urna ut justo feugiat posuere.

Sed ac orci nec orci imperdiet fermentum ut in quam. Curabitur in risus vel mi commodo sollicitudin. Nullam scelerisque velit metus, in pretium quam faucibus eget. Maecenas eu massa non urna dapibus pulvinar. Donec sed orci eu velit elementum efficitur et vitae ex. Nullam id mauris eget lorem condimentum scelerisque a dictum arcu. Proin sed est est. Donec ut pulvinar lectus, et fermentum orci. Vivamus id posuere turpis. Integer et nulla sit amet purus ultrices sodales id convallis erat. Quisque viverra a ex quis elementum. Aenean iaculis in nunc imperdiet porta. Quisque ultrices commodo nibh condimentum eleifend. Donec eu dolor justo. Vestibulum vehicula arcu ut rhoncus imperdiet.

Mauris aliquam cursus quam a malesuada. Curabitur placerat velit nisl, ut aliquam mi auctor at. Ut a ligula tellus. Proin sit amet eleifend elit, eu eleifend ligula. Integer at magna quis erat ullamcorper varius vel in odio. Quisque ultricies erat vitae varius vestibulum. Aenean accumsan venenatis elit, non blandit augue tristique eu. Maecenas malesuada ligula nec massa euismod, nec mollis metus rhoncus.

Phasellus convallis felis dapibus lacus volutpat, in fermentum turpis finibus. Vivamus pulvinar in urna ac consequat. Ut viverra libero ut elit volutpat, in tincidunt ipsum facilisis. Etiam vel congue ipsum. Suspendisse potenti. Vivamus non vestibulum augue, non efficitur purus. Aenean rhoncus fermentum mauris, nec condimentum est volutpat pellentesque.

Sed maximus quam a rhoncus condimentum. Fusce non dolor tellus. In vulputate libero sed lorem euismod fringilla. Sed nibh nunc, faucibus eget egestas ac, tempus in erat. Nam libero justo, pretium in pretium eget, sagittis non lorem. Proin mollis lacinia orci, eu tincidunt purus efficitur nec. Proin eget leo ac lectus cursus efficitur varius ut est. Cras placerat turpis sed justo viverra, nec semper lorem varius. Donec aliquam finibus consectetur. Duis suscipit ut eros quis pulvinar. Proin vulputate ligula non lobortis finibus. Nam at odio lorem. In varius pellentesque cursus. Vivamus lacinia finibus mauris.

Morbi augue massa, congue ut elementum id, pellentesque pretium urna. Nunc sit amet lacus ut nibh facilisis ornare. Quisque vitae lacinia nunc. Donec lobortis nec mi sit amet sollicitudin. Fusce dignissim condimentum sem, in gravida turpis molestie non. Phasellus imperdiet volutpat mi, non convallis lorem blandit ut. Donec sed finibus augue. Nam molestie lectus vitae diam pellentesque egestas. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nullam nec tincidunt dolor, vel tincidunt ligula. Cras magna tellus, feugiat ut egestas ut, ullamcorper vitae lorem. Praesent commodo, metus quis tempus faucibus, libero nisl molestie metus, varius pulvinar eros turpis quis odio. Sed vel fermentum metus, sagittis venenatis tortor.

Nulla a molestie leo. Nullam aliquet sed lectus quis porttitor. Nulla quis tincidunt magna. Vestibulum ut sapien libero. Quisque pulvinar et ex id commodo. Proin non ornare ante. In mollis erat eget felis dignissim, in gravida leo gravida. Donec eu lacus lectus. Aenean iaculis tellus eu lectus sagittis ullamcorper. Vivamus vitae nibh mauris. In eleifend eget ligula vitae sagittis.

Nam at tellus non ante interdum blandit. Integer non lorem sit amet metus ullamcorper efficitur. In hac habitasse platea dictumst. Mauris vehicula turpis eget interdum porta. Morbi libero nisl, mollis in posuere quis, ultrices a urna. Nunc efficitur condimentum arcu, in iaculis lectus ullamcorper vel. In facilisis eleifend mauris nec placerat. Maecenas ac tortor varius, pellentesque turpis laoreet, consequat mauris. Praesent dolor felis, consequat at porta sit amet, volutpat ut ipsum. Pellentesque tempus hendrerit nisl, eu porta nibh porta non. Lorem ipsum dolor sit amet, consectetur adipiscing elit.

Fusce eu magna blandit, suscipit orci at, condimentum lorem. Maecenas nec fermentum libero. Fusce id arcu quam. Quisque vel efficitur dui. Vivamus nunc ante, finibus sed massa vel, sodales aliquam sem. Vestibulum felis odio, aliquam a neque sed, pharetra vulputate ipsum. Mauris sit amet vehicula dolor. Proin lacinia sem id consectetur interdum. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer at neque a orci luctus molestie. Vivamus neque quam, ullamcorper sed turpis cursus, molestie bibendum velit. Aliquam maximus accumsan posuere.

Duis lacus lectus, commodo quis viverra ut, cursus condimentum ex. Sed vitae pulvinar diam. Nunc tincidunt elit id eros vehicula, in vulputate justo faucibus. Quisque tempor, nisi in commodo egestas, mi ex commodo mi, congue bibendum neque est et magna. Duis vel purus mauris. Nam in elementum mauris. Maecenas odio sapien, mollis sed quam non, vulputate rutrum turpis. Phasellus dolor arcu, euismod a efficitur nec, iaculis tempor magna. Quisque id dui hendrerit, blandit nisl eget, porta arcu. Donec at nulla quis diam consectetur vulputate. Pellentesque sed tempus augue.

Mauris nec commodo diam, eu sollicitudin mi. Duis sollicitudin gravida fermentum. Nulla sit amet dolor vel augue consequat semper. Aliquam erat volutpat. Nunc rutrum vulputate malesuada. Cras ullamcorper viverra lorem, quis sodales mi feugiat sed. Duis iaculis porta varius. Fusce quis sem laoreet, aliquet eros eget, malesuada tellus. Cras nulla mi, pulvinar id sapien sit amet, vestibulum viverra dolor. Duis tempor orci eu enim consectetur, nec ullamcorper felis ullamcorper. Maecenas lobortis accumsan tellus, at luctus nisl luctus sed. Maecenas malesuada non ligula et iaculis. Ut molestie, tellus nec pretium eleifend, erat felis convallis massa, ut aliquet augue leo id magna. Nam faucibus libero sed enim cursus bibendum. Mauris non accumsan enim, sit amet imperdiet dolor. Praesent laoreet vulputate tortor.

Curabitur tincidunt vel ipsum et feugiat. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Suspendisse tempus lorem ac accumsan sagittis. Suspendisse id leo sit amet massa eleifend tristique ac vel tellus. In hac habitasse platea dictumst. Maecenas posuere, purus eu fermentum dictum, est urna gravida risus, vitae condimentum nunc quam sed nulla. Fusce in dolor sed magna ornare ultrices. Nulla facilisi. Etiam dui felis, tempus in mauris quis, consectetur commodo quam.

Mauris semper laoreet nibh vitae efficitur. Integer eget magna dui. Ut pellentesque ut metus ac mattis. Cras at ante bibendum, porttitor nisi et, ornare sapien. Nunc lobortis neque sed neque rhoncus volutpat. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Phasellus auctor blandit malesuada. Ut tempor feugiat lorem at lobortis. In finibus lorem vel imperdiet consequat.

Curabitur quis elit non mauris viverra auctor. Sed nec massa turpis. Aliquam feugiat sem a pretium efficitur. Proin sit amet commodo augue. Sed eget finibus justo, a laoreet mi. Ut vestibulum nisi scelerisque, posuere sem at, commodo massa. Suspendisse iaculis ex non augue ornare, eu egestas felis tempor. Curabitur mattis diam in dui lobortis, vel tempor ex pharetra. Ut vel dolor placerat, interdum augue finibus, consequat turpis. Sed sollicitudin ligula vel tincidunt venenatis. Nulla facilisi. Phasellus sollicitudin, metus quis semper accumsan, metus orci varius elit, in consectetur lorem massa faucibus nunc. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Donec eu sapien hendrerit, egestas nulla quis, gravida nisi. In ac mattis libero, ac egestas sem.

Mauris lacus turpis, dictum a leo ac, viverra aliquam ligula. Vivamus at malesuada erat. Etiam quam elit, auctor ut arcu at, varius condimentum est. Integer sed ornare dui, sed vehicula ligula. Vivamus accumsan semper nisi nec mattis. Integer pretium nulla sed mi venenatis rhoncus. Cras non ante porta, gravida ipsum vel, tincidunt nunc.

Etiam molestie purus vel augue aliquet, et mollis mauris bibendum. Nam eleifend velit ut velit tempor sagittis. Proin vehicula est in pretium aliquam. Sed purus sem, varius sed efficitur ut, porta vitae magna. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Quisque congue magna nec ligula tincidunt finibus. Nulla libero leo, imperdiet ac dolor eu, fermentum scelerisque leo. Vestibulum velit nulla, mattis in tincidunt et, commodo at eros. Vivamus dictum sollicitudin eros, vitae ultrices tellus mattis dapibus. Phasellus sed rutrum justo. Integer metus sem, blandit ut imperdiet vel, euismod et dui.

Etiam rhoncus, nisl ut auctor tincidunt, felis justo molestie velit, eu viverra nisl est sed nunc. Donec eu sapien ut augue fermentum placerat nec id dolor. Sed a felis ipsum. Mauris non libero vel risus luctus vehicula ut eu magna. Aenean porta suscipit nibh. Aliquam eu luctus lorem. Fusce eget lacus quis sapien cursus sodales.

Donec leo massa, imperdiet quis turpis ac, porta porta ante. Etiam dictum risus ac nisl pharetra scelerisque. Donec a vestibulum lectus. Nullam tincidunt tincidunt felis a gravida. Donec malesuada metus at sollicitudin pharetra. Duis eu erat ut elit mollis bibendum in eget tellus. Donec suscipit vel mauris at pharetra.

Etiam posuere a diam porttitor facilisis. Donec ut lobortis felis, et condimentum ipsum. Nam accumsan commodo massa non aliquet. Etiam convallis, sapien vel venenatis elementum, nulla neque consectetur velit, ac maximus neque risus eu ligula. Nunc tortor est, rutrum non interdum non, bibendum id lorem. Integer gravida, lectus eget rhoncus rhoncus, justo turpis tincidunt urna, sed facilisis turpis nisi nec neque. Mauris eget accumsan dolor, ut gravida nulla.

Ut faucibus varius massa a eleifend. Nam est arcu, suscipit sit amet nisi in, iaculis euismod augue. Fusce porta eu nunc vitae pretium. Etiam ultrices tincidunt nulla, nec euismod risus. Interdum et malesuada fames ac ante ipsum primis in faucibus. Pellentesque efficitur, purus id varius commodo, libero eros condimentum urna, eu tempus elit quam nec sapien. Phasellus in euismod odio, tempus ornare mauris. Nullam varius enim et tortor consectetur, at tristique nisi ultrices. Aliquam in lacus in mi dictum euismod. Donec a erat ut urna placerat porttitor in at lorem. Phasellus aliquam felis sed justo posuere, non finibus leo tincidunt. Maecenas ut tincidunt elit. Pellentesque ut mollis velit, eu auctor odio. Quisque iaculis sollicitudin lorem, in finibus mi fermentum vitae. Donec vel velit in leo fermentum dignissim ut ut lectus. Aliquam auctor nibh eu eros vehicula, sit amet faucibus justo ornare.

Curabitur dignissim odio dui. In orci sem, facilisis sed scelerisque nec, dictum quis velit. Vivamus eget nisl rhoncus, maximus urna laoreet, gravida felis. Fusce ultricies at turpis viverra elementum. Etiam sit amet ex sed massa maximus blandit sed efficitur orci. Cras tincidunt condimentum risus eu ultricies. Sed volutpat scelerisque dolor, quis aliquam tortor tincidunt tincidunt. Quisque tincidunt malesuada congue.

Donec odio est, fermentum ac cursus vitae, tristique quis dui. Cras tempus fermentum augue vel pulvinar. Aliquam erat volutpat. Donec placerat id eros eu ornare. Curabitur ligula mi, accumsan et diam ut, tempor auctor urna. Aenean viverra mauris vitae orci feugiat dignissim. Donec enim ligula, commodo et elit at, dignissim efficitur orci. Aliquam interdum turpis quis neque pharetra lacinia sit amet non ligula. Nunc lobortis et tellus ac tincidunt. Aliquam venenatis efficitur ligula, vitae commodo nunc rhoncus id. Nam pulvinar pellentesque bibendum. Duis scelerisque tellus eget hendrerit bibendum. Phasellus erat mauris, fringilla sit amet odio vel, cursus vulputate arcu. Integer maximus lacus urna, at vulputate ipsum suscipit at. Aliquam erat volutpat. In congue non magna sit amet tincidunt.

Interdum et malesuada fames ac ante ipsum primis in faucibus. Praesent nec mauris fermentum, mattis diam feugiat, consequat libero. Nam non luctus odio, non ornare sapien. Nunc enim purus, cursus a nibh nec, rhoncus semper diam. Donec commodo turpis in ornare lobortis. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Aenean dolor massa, vestibulum at pretium ac, auctor ut justo. Phasellus a pellentesque turpis. Cras pulvinar porta tempus. Nulla auctor lorem nec urna sagittis, in vestibulum enim dapibus. Integer quis massa sed enim auctor rhoncus eget ac sapien. Nulla vel dictum erat. Sed laoreet gravida rutrum. Suspendisse eget augue tellus.

Quisque facilisis ex vel mi efficitur, at rhoncus lectus iaculis. In eu rutrum libero, in blandit tortor. Suspendisse finibus nisi id nulla accumsan pellentesque. Sed et bibendum purus. In hac habitasse platea dictumst. Praesent id odio aliquet, tincidunt augue quis, pellentesque lectus. Maecenas luctus nec dui a venenatis. Cras venenatis libero cursus, tristique magna eget, egestas felis. Phasellus non suscipit diam, vel pulvinar erat.

Pellentesque metus justo, blandit ut diam sit amet, lacinia vestibulum orci. Curabitur a metus massa. Sed facilisis, elit aliquet tempor lacinia, ipsum ipsum cursus erat, at elementum tortor purus in dui. Vivamus blandit congue libero, sed tempor diam vulputate non. Praesent consequat venenatis lorem ut aliquam. Quisque venenatis, eros ac imperdiet feugiat, orci ipsum feugiat libero, ut fermentum sapien sem ut nunc. Aenean tincidunt porta velit at varius. Nam ornare dolor dapibus est volutpat cursus. Phasellus et luctus erat. Fusce eget mauris faucibus justo pellentesque laoreet sit amet eget tellus. Vestibulum malesuada lorem vel lobortis venenatis. Suspendisse eu molestie justo. Sed congue consequat eleifend.

Integer at condimentum diam. Nullam mi felis, hendrerit sit amet dignissim vitae, ultrices nec erat. Ut ac dictum nulla. Proin facilisis in tortor sed tristique. Etiam fringilla posuere tortor sit amet cursus. Sed dignissim euismod interdum. Praesent nunc tortor, aliquam sit amet tortor ac, sagittis congue ex. Proin felis arcu, interdum sit amet leo in, fermentum egestas massa.

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla feugiat laoreet nibh sit amet tristique. Proin id mollis metus. Praesent consectetur arcu sit amet vulputate convallis. Fusce risus orci, posuere ut varius nec, congue non nibh. Phasellus viverra nunc sem. Curabitur ornare magna in ex iaculis, vel luctus est faucibus. Maecenas vitae felis tellus. Proin nec tempus tortor. Phasellus luctus leo non aliquam lobortis. In quis arcu egestas, lacinia urna ac, laoreet erat. Phasellus tempus lacus eros, sit amet sagittis orci molestie a.

Sed convallis a elit non vestibulum. Nulla laoreet leo id vehicula euismod. Praesent vitae velit sagittis leo placerat dapibus vitae sit amet dolor. Praesent lobortis, justo et ultrices pharetra, purus dui lacinia quam, eget interdum augue diam ut justo. In diam lorem, consequat id velit eu, luctus tempor est. Cras in tempor turpis. Nunc consequat euismod nulla, quis placerat ipsum lobortis eu.

Donec malesuada leo eu nulla consequat faucibus. Ut vitae faucibus enim. Pellentesque a varius lorem. Nulla vel orci vitae sapien lacinia dignissim et at tellus. Duis posuere congue sapien nec placerat. Maecenas ut porttitor nibh. Sed ultrices pulvinar velit, eu laoreet urna facilisis ut. Etiam sodales, felis vitae condimentum efficitur, justo nunc interdum ligula, nec faucibus augue est a lorem. Vestibulum at dolor quis libero tincidunt pellentesque. Aenean fermentum eleifend ex, placerat sagittis velit auctor a. Pellentesque vel laoreet nisl, eu porta magna. Nullam rhoncus vestibulum ipsum, at elementum ex rhoncus id. Suspendisse vitae metus quis ex ullamcorper tincidunt non vulputate nisi. Aliquam erat volutpat. Praesent convallis urna sit amet est fermentum, eu suscipit lectus scelerisque.

Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Pellentesque sodales tellus laoreet odio eleifend, quis pharetra justo pretium. Sed congue justo a viverra blandit. Curabitur lectus quam, scelerisque condimentum eros quis, lacinia pulvinar ipsum. Nam maximus gravida ex, et pharetra ipsum convallis quis. Donec eget ultricies magna. Duis sodales nec nibh id venenatis. Suspendisse in lectus et nisl semper consectetur.

Praesent sed neque posuere leo iaculis maximus. Aliquam eget sapien vitae eros facilisis sagittis at non nisi. Nullam quam ligula, maximus nec consectetur in, volutpat quis augue. Etiam ac justo massa. Etiam vitae justo vitae risus mollis scelerisque. Ut vitae sem efficitur, condimentum lacus vel, congue velit. Duis bibendum, ante sed finibus aliquam, libero velit aliquet felis, vel egestas massa mauris et odio. In hendrerit scelerisque ipsum, vitae congue ex gravida vel.

Pellentesque nec blandit odio. Nam vel orci in lorem pellentesque lobortis vel id mauris. Aliquam rutrum tincidunt justo, id commodo tellus sollicitudin accumsan. Integer vel nunc in lacus feugiat pharetra eu et sapien. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Etiam pulvinar orci augue, vitae porttitor est blandit sollicitudin. In tempor vulputate leo ut vestibulum. Donec ac dictum velit, ac interdum libero. Nam vehicula convallis massa, a ultrices dolor finibus quis. Ut sollicitudin laoreet elit a tincidunt. Pellentesque vitae dui sem.

Duis egestas quam eu dui varius volutpat in vel tortor. Vivamus felis libero, feugiat in ligula id, efficitur porttitor nisi. Aliquam erat volutpat. Morbi quis ligula ornare, mattis nisl interdum, ullamcorper arcu. Phasellus vehicula turpis a cursus sodales. Proin ultricies ex at feugiat gravida. Quisque placerat, justo ut varius auctor, nulla eros tempus magna, non mollis lacus nunc in diam. Vivamus a gravida diam. Nam porta iaculis auctor. Maecenas luctus arcu ac vulputate porttitor. Vestibulum sit amet tempor risus. Donec non pretium ex.

Maecenas nec sapien turpis. Mauris facilisis lacinia ligula, vehicula tempor enim malesuada quis. Cras consectetur, augue vitae porttitor scelerisque, enim ex luctus eros, sit amet varius justo nisl at ex. Vivamus sed pellentesque nisl. Etiam blandit est et lacinia suscipit. Nulla consequat, lectus ac faucibus viverra, elit ipsum porttitor arcu, eu ultrices felis dui ut nulla. Quisque dignissim neque pellentesque dui auctor efficitur. Donec ullamcorper dignissim odio nec mollis. In vitae nibh fringilla, semper est ac, euismod augue. Vivamus faucibus purus vel nulla viverra fermentum. Duis quis nibh ligula. Nulla facilisi. Sed condimentum dolor at lacus finibus tincidunt. Suspendisse porttitor dui id mattis semper. Phasellus elementum iaculis cursus.

Pellentesque commodo lacinia vestibulum. Sed eu diam justo. Sed vitae lacinia felis. Curabitur fermentum mattis accumsan. Fusce sed ipsum sit amet mi feugiat tincidunt. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Sed interdum justo non gravida feugiat. Cras posuere elit lorem, eget rutrum lectus tincidunt vitae. Proin ut justo eu massa euismod sagittis nec nec arcu. Integer nec magna nec erat consectetur faucibus imperdiet ac velit. Cras sit amet consequat tortor. Nulla semper, mauris quis pretium vehicula, leo massa volutpat purus, eu tincidunt quam sem vel dolor. Nunc finibus urna lacus. Fusce elementum ultrices aliquet. Nam sit amet diam a massa porttitor placerat blandit sed justo. Vivamus id metus porttitor, vestibulum ex eget, fermentum risus.

Nullam a nisl dapibus, molestie mi in, faucibus turpis. Cras pellentesque aliquet scelerisque. Cras sit amet bibendum orci. Aliquam et dapibus est, quis aliquam massa. Pellentesque fringilla eros nec nisl condimentum, eget consectetur lorem malesuada. Aenean sit amet urna vel sapien eleifend varius ac sed eros. Aliquam eu purus a nisl vulputate luctus. Etiam et odio vitae nunc fermentum scelerisque at ut lorem. Duis gravida turpis ut enim gravida, eget feugiat risus suscipit. Cras eros neque, gravida ac ex a, vehicula tempus dui. In mi urna, vestibulum ac odio at, blandit iaculis tellus. Integer a ligula ante. Donec semper convallis ex, et cursus diam finibus eu. Maecenas vitae quam vel erat feugiat egestas. Aliquam eu rutrum est.

Sed et nisi dui. Fusce non congue urna. Phasellus libero felis, vehicula a nunc eget, accumsan venenatis est. Praesent id scelerisque erat. Phasellus eu augue vel velit volutpat semper. Curabitur facilisis laoreet quam, vel efficitur est dignissim non. Etiam non massa ac velit scelerisque sodales ut a lectus.

Nulla accumsan libero vitae turpis iaculis volutpat. Nullam laoreet, felis id ultrices gravida, felis libero sollicitudin tellus, nec varius lectus massa in velit. Etiam sagittis sodales nisl, non ullamcorper lectus scelerisque sit amet. Sed ut urna feugiat, mollis elit vitae, gravida est. Praesent cursus consequat turpis, at consectetur sapien lobortis non. Sed rutrum odio arcu, in ultricies nunc venenatis eleifend. Vivamus non congue leo, eu tempus erat. Maecenas sed felis convallis, tristique elit sit amet, ultrices ligula. Aenean aliquet lacus at urna sagittis, vel fermentum enim tincidunt.

Curabitur tincidunt felis in pretium vestibulum. Praesent nec diam eu odio fringilla tempor eget vel nulla. In egestas massa libero, id maximus turpis euismod vel. Nullam ipsum mi, pulvinar at faucibus id, consectetur sit amet orci. Suspendisse mollis luctus dolor vel sollicitudin. Vivamus finibus dui et diam pulvinar aliquam. Nulla non enim convallis, sagittis metus vitae, sollicitudin sapien. Pellentesque in faucibus ex, non congue mauris. Nulla ut ante id sapien interdum vulputate. Morbi dolor libero, egestas vitae vehicula eu, sagittis in nulla. Maecenas viverra est nunc. Morbi eleifend egestas justo, sed vehicula purus sodales id. Cras ac dui nisi. Donec viverra sem dolor, sed mollis nisl pulvinar a. Aliquam malesuada sapien id nisl rutrum fringilla.

Praesent in massa auctor, egestas lacus in, volutpat dui. Donec tincidunt pharetra diam vel feugiat. Morbi sit amet arcu sodales, convallis augue at, malesuada ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Quisque eget vehicula tortor, sit amet sollicitudin enim. Fusce pretium est ligula, vitae porttitor tellus maximus et. Vivamus erat mi, cursus sit amet posuere eget, iaculis non odio. Sed vulputate diam et interdum auctor. Nulla velit sapien, maximus non posuere nec, ornare ultrices risus. Proin ac pharetra nibh.

Duis feugiat mattis dolor, a suscipit nisl fringilla et. Sed lobortis vulputate quam id pulvinar. Donec commodo, quam id viverra interdum, lorem lacus gravida felis, non auctor est eros a tellus. Fusce pretium turpis id interdum dictum. Aliquam volutpat ultrices nisi. Mauris tincidunt purus risus, eu lobortis urna interdum vel. Phasellus augue massa, dignissim nec auctor eget, hendrerit sed quam. Ut sagittis vitae est non tristique. Vivamus malesuada gravida nisi, non sollicitudin arcu efficitur eget. Nam id fringilla enim. Donec vulputate, mauris vel ornare pellentesque, mauris sem elementum leo, sit amet laoreet metus odio nec massa. Nam eu fringilla erat, ornare sodales metus.";

        public readonly string XmlMessage = @"<?xml version=""1.0""?>
            <catalog>
               <book id=""bk101"">
                  <author>Gambardella, Matthew</author>
                  <title>XML Developer's Guide</title>
                  <genre>Computer</genre>
                  <price>44.95</price>
                  <publish_date>2000-10-01</publish_date>
                  <description>An in-depth look at creating applications
                  with XML.</description>
               </book>
               <book id=""bk102"">
                  <author>Ralls, Kim</author>
                  <title>Midnight Rain</title>
                  <genre>Fantasy</genre>
                  <price>5.95</price>
                  <publish_date>2000-12-16</publish_date>
                  <description>A former architect battles corporate zombies,
                  an evil sorceress, and her own childhood to become queen
                  of the world.</description>
               </book>
               <book id = ""bk103"" >
                  < author > Corets, Eva</author>
                  <title>Maeve Ascendant</title>
                  <genre>Fantasy</genre>
                  <price>5.95</price>
                  <publish_date>2000-11-17</publish_date>
                  <description>After the collapse of a nanotechnology
                  society in England, the young survivors lay the
                  foundation for a new society.</description>
               </book>
               <book id=""bk104"">
                  <author>Corets, Eva</author>
                  <title>Oberon's Legacy</title>
                  <genre>Fantasy</genre>
                  <price>5.95</price>
                  <publish_date>2001-03-10</publish_date>
                  <description>In post-apocalypse England, the mysterious
                  agent known only as Oberon helps to create a new life 
                  for the inhabitants of London.Sequel to Maeve
                  Ascendant.</description>
               </book>
               <book id = ""bk105"" >
                  < author > Corets, Eva</author>
                  <title>The Sundered Grail</title>
                  <genre>Fantasy</genre>
                  <price>5.95</price>
                  <publish_date>2001-09-10</publish_date>
                  <description>The two daughters of Maeve, half-sisters,
                  battle one another for control of England.Sequel to
                  Oberon's Legacy.</description>
               </book>
               <book id = ""bk106"" >
                  < author > Randall, Cynthia</author>
                  <title>Lover Birds</title>
                  <genre>Romance</genre>
                  <price>4.95</price>
                  <publish_date>2000-09-02</publish_date>
                  <description>When Carla meets Paul at an ornithology
                  conference, tempers fly as feathers get ruffled.</description>
               </book>
               <book id = ""bk107"" >
                  < author > Thurman, Paula</author>
                  <title>Splish Splash</title>
                  <genre>Romance</genre>
                  <price>4.95</price>
                  <publish_date>2000-11-02</publish_date>
                  <description>A deep sea diver finds true love twenty
                  thousand leagues beneath the sea.</description>
               </book>
               <book id = ""bk108"" >
                  < author > Knorr, Stefan</author>
                  <title>Creepy Crawlies</title>
                  <genre>Horror</genre>
                  <price>4.95</price>
                  <publish_date>2000-12-06</publish_date>
                  <description>An anthology of horror stories about roaches,
                  centipedes, scorpions and other insects.</description>
               </book>
               <book id = ""bk109"" >
                  < author > Kress, Peter</author>
                  <title>Paradox Lost</title>
                  <genre>Science Fiction</genre>
                  <price>6.95</price>
                  <publish_date>2000-11-02</publish_date>
                  <description>After an inadvertant trip through a Heisenberg
                  Uncertainty Device, James Salway discovers the problems
                  of being quantum.</description>
               </book>
               <book id = ""bk110"" >
                  < author > O'Brien, Tim</author>
                  <title>Microsoft.NET: The Programming Bible</title>
                  <genre>Computer</genre>
                  <price>36.95</price>
                  <publish_date>2000-12-09</publish_date>
                  <description>Microsoft's .NET initiative is explored in 
                  detail in this deep programmer's reference.</description>
               </book>
               <book id = ""bk111"" >
                  < author > O'Brien, Tim</author>
                  <title>MSXML3: A Comprehensive Guide</title>
                  <genre>Computer</genre>
                  <price>36.95</price>
                  <publish_date>2000-12-01</publish_date>
                  <description>The Microsoft MSXML3 parser is covered in 
                  detail, with attention to XML DOM interfaces, XSLT processing,
                  SAX and more.</description>
               </book>
               <book id = ""bk112"" >
                  < author > Galos, Mike</author>
                  <title>Visual Studio 7: A Comprehensive Guide</title>
                  <genre>Computer</genre>
                  <price>49.95</price>
                  <publish_date>2001-04-16</publish_date>
                  <description>Microsoft Visual Studio 7 is explored in depth,
                  looking at how Visual Basic, Visual C++, C#, and ASP+ are 
                  integrated into a comprehensive development
                  environment.</description>
               </book>
            </catalog>";

        #endregion

        public string GetString() => StringMessage;

        public string GetXmlString() => XmlMessage;

        #region Benchmark Accessors

        [Benchmark]
        public string SanitizeWithCharRange() => RemoveInvalidXMLCharsWithinCharRange(GetString());

        [Benchmark]
        public string SanitizeWithXmlConvert() => RemoveInvalidXmlCharsWithXmlConvert(GetString());

        [Benchmark]
        public string SanitizeWithXmlConvertLinq() => RemoveInvalidXmlCharsWithXmlConvertLinq(GetString());

        [Benchmark]
        public string SanitizeWithRangeAndXmlConvert() => RemoveInvalidXmlCharsWithXmlConvert(GetString());

        [Benchmark]
        public string SanitizeWithXmlConvertAndStringBuilder() => RemoveInvalidXmlCharsWithXmlConvertStringBuilder(GetString());

        [Benchmark]
        public string SanitizeWithRangeAndStringBuilder() => RemoveInvalidXMLCharsWithinCharRangeAndStringBuilder(GetString());

        [Benchmark]
        public string SanitizeWithRegex() => RemoveInvalidXMLCharsWithRegex(GetString());

        [Benchmark]
        public string SanitizeWithCompiledRegex() => RemoveInvalidXmlCharsWithCompiledRegex(GetString());

        /// <summary>
        /// This method only works for well formed xml strings
        /// </summary>
        [Benchmark]
        public bool ValidateViaXmlCreation() => ValidateWithXmlCreation(GetXmlString());

        #endregion

        /// <summary>
        /// Based on OpenLiveWriter code from Microsoft. This will be used as the baseline
        /// </summary>
        public static string RemoveInvalidXMLCharsWithinCharRange(string input)
        {
            int index = 0;
            char[] result = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                // Is the character from the valid XML character ranges?
                if ((input[i] >= 9 && input[i] <= 10) ||
                    (input[i] == 13) ||
                    (input[i] >= 32 && input[i] <= 55295) ||
                    (input[i] >= 57344 && input[i] <= 65533))
                {
                    result[index++] = input[i];
                }
            }

            return new string(result, 0, index);
        }

        public static string RemoveInvalidXMLCharsWithRangeAndConvert(string input)
        {
            int index = 0;
            char[] result = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                // Is the character from the valid XML character ranges?
                if ((input[i] >= 9 && input[i] <= 10) ||
                    (input[i] == 13) ||
                    (input[i] >= 32 && input[i] <= 55295) ||
                    (input[i] >= 57344 && input[i] <= 65533))
                {
                    if (XmlConvert.IsXmlChar(input[i]))
                    {
                        result[index++] = input[i];
                    }
                }
            }

            return new string(result, 0, index);
        }

        public static string RemoveInvalidXmlCharsWithXmlConvert(string input)
        {
            int index = 0;
            char[] result = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                // Is the character from the valid XML character ranges?
                if (XmlConvert.IsXmlChar(input[i]))
                {
                    result[index++] = input[i];
                }
            }

            return new string(result, 0, index);
        }

        public static string RemoveInvalidXmlCharsWithXmlConvertLinq(string text)
        {
            var validXmlChars = text.Where(XmlConvert.IsXmlChar).ToArray();
            return new string(validXmlChars);
        }

        public static string RemoveInvalidXmlCharsWithXmlConvertStringBuilder(string text)
        {
            if (text == null) return text;
            if (text.Length == 0) return text;

            // a bit complicated, but avoids memory usage if not necessary
            StringBuilder result = null;
            for (int i = 0; i < text.Length; i++)
            {
                var ch = text[i];
                if (XmlConvert.IsXmlChar(ch))
                {
                    if (result != null) result.Append(ch);
                }
                else
                {
                    if (result == null)
                    {
                        result = new StringBuilder();
                        result.Append(text.Substring(0, i));
                    }
                }
            }

            return result == null ? text : result.ToString();
        }

        public static string RemoveInvalidXMLCharsWithinCharRangeAndStringBuilder(string text)
        {
            if (text == null) return text;
            if (text.Length == 0) return text;

            // a bit complicated, but avoids memory usage if not necessary
            StringBuilder result = null;
            for (int i = 0; i < text.Length; i++)
            {
                if ((text[i] >= 9 && text[i] <= 10) ||
                    (text[i] == 13) ||
                    (text[i] >= 32 && text[i] <= 55295) ||
                    (text[i] >= 57344 && text[i] <= 65533))
                {
                    if (result != null) result.Append(text[i]);
                }
                else
                {
                    if (result == null)
                    {
                        result = new StringBuilder();
                        result.Append(text.Substring(0, i));
                    }
                }
            }

            return result == null ? text : result.ToString();
        }

        public static string RemoveInvalidXMLCharsWithRegex(string tmpContents, string xmlVersion = "1.0")
        {
            string pattern = string.Empty;
            switch (xmlVersion)
            {
                case "1.0":
                    pattern = @"#x((10?|[2-F])FFF[EF]|FDD[0-9A-F]|7F|8[0-46-9A-F]9[0-9A-F])";
                    break;
                case "1.1":
                    pattern = @"#x((10?|[2-F])FFF[EF]|FDD[0-9A-F]|[19][0-9A-F]|7F|8[0-46-9A-F]|0?[1-8BCEF])";
                    break;
                default:
                    throw new ArgumentException(nameof(xmlVersion));
            }

            Regex regex = new Regex(pattern,
                RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
            if (regex.IsMatch(tmpContents))
            {
                regex.Replace(tmpContents, string.Empty);
            }

            return tmpContents;
        }

        public static string RemoveInvalidXmlCharsWithCompiledRegex(string tmpContents, string xmlVersion = "1.0")
        {
            string pattern = string.Empty;
            switch (xmlVersion)
            {
                case "1.0":
                    pattern = @"#x((10?|[2-F])FFF[EF]|FDD[0-9A-F]|7F|8[0-46-9A-F]9[0-9A-F])";
                    break;
                case "1.1":
                    pattern = @"#x((10?|[2-F])FFF[EF]|FDD[0-9A-F]|[19][0-9A-F]|7F|8[0-46-9A-F]|0?[1-8BCEF])";
                    break;
                default:
                    throw new ArgumentException(nameof(xmlVersion));
            }

            Regex regex = new Regex(pattern,
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            if (regex.IsMatch(tmpContents))
            {
                regex.Replace(tmpContents, string.Empty);
            }

            return tmpContents;
        }

        public static bool ValidateWithXmlCreation(string tmpContents)
        {
            XmlDocument xml = new XmlDocument();

            try
            {
                xml.LoadXml(tmpContents);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}